package net.sf.jode.decompiler;

import java.util.Hashtable;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.SortedMap;
import java.util.TreeMap;

import net.sf.jode.GlobalOptions;
import net.sf.jode.bytecode.ClassPath;

/**
 * Generate header that says: "using X;"
 * Instead of: "import X;"
 * 
 * <p>Code is mostly copy-and-paste from super-class.
 * 
 * FIXME: Not nice to put this class in a Jode package.
 * FIXME: Somehow avoid this massive code duplication.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class RoopsImportHandler extends ImportHandler {

	/**
	 * Constructor
	 * 
	 * @param classPath
	 */
  public RoopsImportHandler(ClassPath classPath, int packageLimit, int classLimit) {
  	super(classPath, packageLimit, classLimit);
  }

	
	protected boolean conflictsImport(String name) {
		int pkgdelim = name.lastIndexOf('.');
		if (pkgdelim != -1) {
			String pkgName = name.substring(0, pkgdelim);
			/* All classes in this package doesn't conflict */
			if (pkgName.equals(pkg))
				return false;

			// name without package, but _including_ leading dot.
			name = name.substring(pkgdelim); 

			if (pkg.length() != 0) {
				/* Does this conflict with a class in this package? */
				if (classPath.existsClass(pkg+name))
					return true;
			} else {
				/* Does this conflict with a class in this unnamed
				 * package? */
				if (classPath.existsClass(name.substring(1)))
					return true;
			}

			Iterator iter = imports.keySet().iterator();
			while (iter.hasNext()) {
				String importName = (String) iter.next();
				if (importName.endsWith(".*")) {
					/* strip the "*" */
					importName = importName.substring
					(0, importName.length()-2);
					if (!importName.equals(pkgName)) {
						if (classPath.existsClass(importName+name))
							return true;
					}
				} else {
					/* Is this a class import with same name? */
					if (importName.endsWith(name)
							|| importName.equals(name.substring(1)))
						return true;
				}
			}
		}
		return false;
	}


	protected void cleanUpImports() {
		Integer dummyVote = new Integer(Integer.MAX_VALUE);
		SortedMap newImports = new TreeMap(comparator);
		List classImports = new LinkedList();
		Iterator iter = imports.keySet().iterator();
		while (iter.hasNext()) {
			String importName = (String) iter.next();
			Integer vote = (Integer) imports.get(importName);
			if (!importName.endsWith(".*")) {
				if (vote.intValue() < importClassLimit)
					continue;
				int delim = importName.lastIndexOf(".");

				if (delim != -1) {
					/* Since the imports are sorted, newImports already
					 * contains the package if it should be imported.
					 */
					if (newImports.containsKey
							(importName.substring(0, delim)+".*"))
						continue;

					/* This is a single Class import, that is not
					 * superseeded by a package import.  Mark it for
					 * importation, but don't put it in newImports, yet.  
					 */
					classImports.add(importName);
				} else if (pkg.length() != 0) {
					/* This is a Class import from the unnamed
					 * package.  It must always be imported.
					 */
					newImports.put(importName, dummyVote);
				}
			} else {
				if (vote.intValue() < importPackageLimit)
					continue;
				newImports.put(importName, dummyVote);
			}
		}

		imports = newImports;
		cachedClassNames = new Hashtable();
		/* Now check if the class import conflict with any of the
		 * package imports.
		 */
		iter = classImports.iterator();
		while (iter.hasNext()) {
			/* If there are more than one single class imports with
			 * the same name, exactly the first (in sorted order) will
			 * be imported. 
			 */
			String classFQName = (String) iter.next();
			if (!conflictsImport(classFQName)) {
				imports.put(classFQName, dummyVote);
				String name = 
					classFQName.substring(classFQName.lastIndexOf('.')+1);
				cachedClassNames.put(classFQName, name);
			}
		}
	}

	public void dumpHeader(TabbedPrintWriter writer) 
	throws java.io.IOException
	{
		writer.println("/* "+ className 
				+ " - Decompiled by JODE");
		writer.println(" * Visit "+GlobalOptions.URL);
		writer.println(" */");
		
		/* replaced: "package X;"
		 * with: "namespace X;" */
		if (pkg.length() != 0)
			writer.println("namespace "+pkg+";");

		cleanUpImports();
		Iterator iter = imports.keySet().iterator();
		String lastFirstPart = null;
		while (iter.hasNext()) {
			String pkgName = (String)iter.next();
			if (!pkgName.equals("java.lang.*")) {
				int firstDot = pkgName.indexOf('.');
				if (firstDot != -1) {
					String firstPart = pkgName.substring(0, firstDot);
					if (lastFirstPart != null
							&& !lastFirstPart.equals(firstPart)) {
						writer.println("");
					}
					lastFirstPart = firstPart;
				}
				// following is the only line changed :)
				writer.println("using "+pkgName+";");
			}
		}
		writer.println("");
	}
}
