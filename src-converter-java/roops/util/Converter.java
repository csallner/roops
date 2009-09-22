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
	
	protected static String rebuildLine(String line, String methodName, String params, String numberOfGoals)
	{
		if(line.contains("WriteToFile(\""))
		{
			return line.replace("WriteToFile(\"", "WriteToFile(\"Method: " + methodName + params + " -- Number of Goals: " + numberOfGoals + " -- ");
		}
		
		return line;
	}
	
	protected static void transformFile(File fileIn, File fileOut) {
		BufferedReader in = null;
		BufferedWriter out = null;
		String methodName = null;
		String numberOfGoals = null;
		String params = null;
		String className = null;
		Boolean classKeywordFound = false; 
		
		try{
			in = new BufferedReader(new FileReader(fileIn));
			out = new BufferedWriter(new FileWriter(fileOut));
			while(in.ready()){ 
				
				String line = in.readLine();

				if(line.matches("\\s*//\\$goals\\s*(\\d+)"))
				{
					numberOfGoals = line.replaceAll("\\s*//\\$goals\\s*(\\d+)", "$1");
				}
				
				else if(line.matches("\\s*public\\s*void\\s*[a-zA-Z_0-9]+\\s*([\\s\\S]*)\\s*"))
				{
					 methodName = line.replaceAll("\\s*public\\s*void\\s*([a-zA-Z_0-9]+)\\s*([\\s\\S]*)\\s*", "$1");
					 params = line.replaceAll("\\s*public\\s*void\\s*[a-zA-Z_0-9]+\\s*([\\s\\S]*+)\\s*", "$1");
				}

				else if(line.matches("\\s*public\\s*class\\s*[a-zA-Z_0-9]+\\s*"))
				{
					 className = line.replaceAll("\\s*public\\s*class\\s*([a-zA-Z_0-9]+)\\s*", "$1");
					 classKeywordFound = true;
				}

				line = transformLine(line);	
				line = rebuildLine(line, methodName, params, numberOfGoals);
				
				if(classKeywordFound)
				{
					if(line.contains("{"))
					{
						line = line.replace("{", "{\\n    private void WriteToFile(string textToWrite){ string outputFileFullPath = \"c:\\\\" + className + ".txt\";  System.IO.StreamWriter log_out;  try{log_out = new System.IO.StreamWriter(outputFileFullPath, true);}  catch(System.IO.IOException exc){System.Console.WriteLine(\"Error: \" + exc.Message + \"Cannot open file.\");return;}  System.Console.SetOut(log_out);System.Console.WriteLine(textToWrite);log_out.Close();  }");
						classKeywordFound = false;
					}
				}
				
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
