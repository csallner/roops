package roops.util;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.util.LinkedList;
import java.util.List;


/**
 * Converts a generic Roops benchmark file to a Roops Java file.
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class Converter {
	
	protected final static List<RoopsPattern> replacements = 
		new LinkedList<RoopsPattern>();
	
	protected static String fileExtensionOut = null;
	
	
	protected static String transformLine(String line) {
		for (RoopsPattern p: replacements)
			line = line.replaceAll(p.pattern, p.replace);
		if (line.startsWith("package"))		// hack to force lower-case package name
			line = line.toLowerCase();
		return line;
	}
	
	
	protected static void transformFile(File fileIn, File fileOut) {
		BufferedReader in = null;
		BufferedWriter out = null;
		
		try{
			in = new BufferedReader(new FileReader(fileIn));
			out = new BufferedWriter(new FileWriter(fileOut));
			while(in.ready()){ 
				String line = in.readLine();
				line = transformLine(line);
				out.write(line);
				out.newLine();
			} 
			System.out.println("File written to "+fileOut.getName()); 
		}
		catch (Exception e){
			e.printStackTrace();
		}  
		try {
			in.close();
		} catch (IOException e) {
			e.printStackTrace();
		}
		try {
			out.close();
		} catch (IOException e) {
			e.printStackTrace();
		}			
	}
	
	protected static String changeExtension(String fileName) {
		if (fileExtensionOut == null)
			return fileName;
			
		int extIndex = fileName.lastIndexOf('.');
		if (extIndex < 0)
			return fileName;
		
		return fileName.substring(0, extIndex) + "." + fileExtensionOut;
	}
	
	
	protected static void transformDir(File parentIn, File parentOut) {
		File[] filesIn = parentIn.listFiles();
		for (File fileIn: filesIn) {
			String simpleName = changeExtension(fileIn.getName());
			String fullNameOut = parentOut.getPath()+java.io.File.separator+simpleName;
			File fileOut = new File(fullNameOut);
			if (fileIn.isDirectory()) {
				if (!fileOut.exists())
					fileOut.mkdir();
				transformDir(fileIn, fileOut);
			}
			else
				transformFile(fileIn, fileOut);
		}
	}
	
	
	protected static void transformFileOrDir(String inFileName, String outFileName) {
		File fileIn = new File(inFileName);
		File fileOut = new File(outFileName);
		
		if(! fileIn.exists()){ 
			System.out.println("Input file or directory does not exist."); 
			return;
		} 
		
		if (fileIn.isDirectory()!=fileOut.isDirectory()) {
			System.out.println(usage);
			return;
		}
		
		if (fileIn.isDirectory()) {
			if (!fileOut.exists())
				fileOut.mkdir();
			transformDir(fileIn, fileOut);
		}
		else
			transformFile(fileIn, fileOut);
	}
	
	
	protected static void loadRules(String inFileName) {
		File fileIn = new File(inFileName);
		BufferedReader in;

		try{
			if(! fileIn.exists()){ 
				System.out.println("Rules file does not exist."); 
				return;
			} 
			in = new BufferedReader(new FileReader(fileIn));
			
			while(in.ready()){
				String pattern = in.readLine();
				String replace = in.readLine();
				replacements.add(new RoopsPattern(pattern, replace));
			}
		}
		catch (Exception e){
			e.printStackTrace();
		}  
	}
	
	
	protected static class RoopsPattern {
		protected final String pattern;
		protected final String replace;
		protected RoopsPattern(String pattern, String replace) {
			if (pattern==null || replace==null)
				throw new NullPointerException();
			this.pattern = pattern;
			this.replace = replace;
		}
	}
	
	protected static final String usage = 
		"USAGE: one of the following\n" +
		"  java roops.util.Converter InFileName OutFileName TransformRulesFileName\n" +
		"  java roops.util.Converter InDirectory OutDirectory TransformRulesFileName OutFileExt\n" +
		"EXAMPLE:\n" +
		"  java roops.util.Converter src-benchmarks-roops src-benchmarks-java roops2java.txt java";
	
	/**
	 * Entry
	 */
	public static void main(String[] args) {
		if ((args.length==0) || "-help".equals(args[0])) {
			System.out.println(usage);
			return;
		}
		
		if (args.length > 3)
			fileExtensionOut = args[3];
		
		loadRules(args[2]);
		transformFileOrDir(args[0], args[1]);
	}
}
