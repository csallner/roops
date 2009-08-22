package edu.uta.cse.roops.proggen.combination;

import java.io.BufferedWriter;
import java.io.FileWriter;
import java.util.Random;

import edu.uta.cse.roops.proggen.bv32.ast.Node;
import edu.uta.cse.roops.proggen.bv32.ast.RepositoryOfNodes;
import edu.uta.cse.roops.proggen.bv32.ast.Tree;

/*
 *  Combined Category (Arithmetic + ArrayWithoutException)
 *  No Argument Necessary in the command line. Fixed format.
 *  
 *  @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 * 
 */

public class CombinedCategory {
	
	public static void main(String [] args){
		
		String output = "";
		RepositoryOfNodes  repos = new RepositoryOfNodes();
		
		try{
			// Create file
			 FileWriter fstream = new FileWriter("CombinedCategory.cs");
			 BufferedWriter fileOut = new BufferedWriter(fstream);
			 

			 output += "//$category roops.extended.Combined \n\n";
			 output += " // Author: Ishtiaque Hussain\n\n";
//			 // RoopsArray.getLength
//			 output += "public class RoopsArray\n";
//			 output += "{\n\tpublic static int getLength(int []a)\n";
//			 output += "\t{\n\t\treturn a.Length;\n\t}\n}\n";
			 
			 output += "//$benchmarkclass\n";
			 output += "public class CombinedCategory\n{\n\n";
			 
			 for(Node m : repos.rootNodes){
					output += "\n\n\t#region "+m.name +"\n";
					m.left = new Node("a[i]","Value_At_Index_i"); //node a[i]
					for(Node n: repos.internalNodes){
						n.left = repos.leafNodes.get(0);   //node x
						n.right = repos.leafNodes.get(1); // node y
						m.right = n;
						
						//createFileBody(m);
						Tree tr = new Tree(m);
						
						output += "\t//$goals 3"+"\n";
						output += "\t//$benchmark" +"\n";
						output += "\tpublic void " + tr.MethodName(tr.Root)+ "( int[] a, int i, int x, int y )"+"\n";
						output += "\t{"+"\n";
						
						output += "\t\tif( (a != null) && (i >= 0) && (i < RoopsArray.getLength(a)) )\n\t\t{\n";
						/*
						 * Randomly changing variables with constants
						 */
						Random randomGenerator = new Random();
						int Range = 32767;
						int RandomInt = randomGenerator.nextInt(2);
						if( RandomInt == 1){ // replace variable by constants
							RandomInt = randomGenerator.nextInt(2);
							switch(RandomInt){
							case 0: output += "\t\t\tx = "+ Integer.toString(randomGenerator.nextInt(Range)) + ";\n";
								break;
							case 1: output += "\t\t\ty = "+ Integer.toString(randomGenerator.nextInt(Range)) + ";\n";
							}
						}
						
						output += "\t\t\tif"+tr.printCondition(tr.Root)+"\n"; // Omitted the 'if' brackets
						output += "\t\t\t\t{ /*$goal 0 */}"+"\n";
						output += "\t\t\telse"+"\n";
						output += "\t\t\t\t{ /*$goal 1*/}"+"\n";
						output +="\t\t}"+"\n";
						
						output += "\t\telse\n";
						output += "\t\t\t{ /*$goal 2*/}"+"\n\t}\n\n";
						
						
												
					}
					output +="\t"+"#endregion\n";
										
				}
			 output +="\n"+ "}"+"\n"+"//$endcategory  roops.extended.Combined";
			 
			 
			 
			 
			 fileOut.write(output);
			 fileOut.close();
			 System.out.println("File Successfully written!");
			
		}catch(Exception e){
			System.err.println(e.getMessage());
			
		}
	}

}
