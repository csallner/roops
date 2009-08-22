package edu.uta.cse.roops.proggen.bv32;

import java.io.*;
import java.util.Random;

import edu.uta.cse.roops.proggen.bv32.ast.Node;
import edu.uta.cse.roops.proggen.bv32.ast.RepositoryOfNodes;
import edu.uta.cse.roops.proggen.bv32.ast.Tree;

/*
 * Holds the main method and creates the output file.
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 */

public class Bv32GenFixed {
	
	private static String output="";
	private static Random randomGenerator = new Random();	//Randomly changing variables with constants
	
	private static void createFileBody(Node m){
		
		Tree tr = new Tree(m);
		
		output += "\t//$goals 2"+"\n";
		output += "\t//$benchmark" +"\n";
		output += "\tpublic void " + tr.MethodName(tr.Root)+ "( int x, int y, int z)"+"\n";
		output += "\t{"+"\n";
		 		
		int Range = 32767;
		int RandomInt = randomGenerator.nextInt(2);
		if( RandomInt == 1){ // replace variable by constants
			RandomInt = randomGenerator.nextInt(3);
			switch(RandomInt){
			case 0: output += "\t\tx = "+ Integer.toString(randomGenerator.nextInt(Range)) + ";\n";
				break;
			case 1: output += "\t\ty = "+ Integer.toString(randomGenerator.nextInt(Range)) + ";\n";
				break;
			case 2: output += "\t\tz = "+ Integer.toString(randomGenerator.nextInt(Range)) + ";\n";
			}
		}
		
		output += "\t\tif"+tr.printCondition(tr.Root)+"\n"; // Omitted the 'if' brackets
		output += "\t\t\t{ /*$goal 0 */}"+"\n";
		output += "\t\telse"+"\n";
		output += "\t\t\t{ /*$goal 1*/}"+"\n";
		output +="\t}"+"\n\n";
	}
	
/*
 * main() parameters {'CategoryName' 'ClassName' 'NoOfMethodsToPrint' 'NoOfMethodsPerFile'}
 * 
 * " Roops.Arithmetic.Integer.Bits32.NoExceptions"
 *	"LinearWithoutOverflow"
 *	100
 *	25
 */
	public static void main(String [] args){

		boolean flagPrintAll = false;
		int NoOfMethodsToPrint;
		int MethodsPerFile;
		String CategoryName;
		String ClassName;
		
		if(args.length !=4){
			String usage =
				"WARNING: This program runs for long time and creates a file containing huge number of methods.\n" +
				"USAGE: java edu.uta.cse.roops.proggen.AllCombinations CategoryName ClassName NoOfMethodsToPrint NoOfMethodsPerFile\n"+
				"\tPlease provide the parameters.\n"+
				"Do you wish ot continue without providing the parameters instead ?? (Y/N):";
			
			System.out.println(usage);
						
			try {
				if((char)System.in.read()=='N')
					return;
				else{
					flagPrintAll = true;
					
				}
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
		
		if(flagPrintAll){
			NoOfMethodsToPrint = Integer.MAX_VALUE;
			MethodsPerFile = Integer.MAX_VALUE;
			CategoryName ="roops.extended.bv32";
			ClassName = "AllCombinations";
			
		}
		else{
			CategoryName = args[0];
			ClassName = args[1];
			NoOfMethodsToPrint = Integer.parseInt(args[2]);  
			MethodsPerFile = Integer.parseInt(args[3]);
		}
		
		RepositoryOfNodes  repos = new RepositoryOfNodes();
		
		int MethodCounter = 0;
		int FileCounter =1;
		int FileMethodCounter = 0;
		
		boolean FlagDone = false ; // We are done printing methods
		
		 try{
			 // Create file
			 FileWriter fstream = new FileWriter(ClassName+ Integer.toString(FileCounter)+".cs");
			 BufferedWriter fileOut = new BufferedWriter(fstream);
			 
			
			 output += "//$category "+CategoryName+"\n\n";
			 output += "//$benchmarkclass"+"\n";
			 output += "public class "+ ClassName+Integer.toString(FileCounter)+"\n"+"{"+"\n";
			 
			 FileCounter++;
			 
			/* main logic
			 * 
			 * for each root node,
			 * 		 put a leaf node (z) as right child,
			 * 		 put an internal node as left child
			 * 				for each internal node
			 * 					put leaf nodes x and y as right and left child respectively
			 * 		  
			 *  
			 */
		
		for(Node m : repos.rootNodes){
			output += "\n\n\t#region "+m.name +"\n";
			m.right = repos.leafNodes.get(2); //node Z
			for(Node n: repos.internalNodes){
				n.left = repos.leafNodes.get(0);   //node x
				n.right = repos.leafNodes.get(1); // node y
				m.left = n;
				
				createFileBody(m);

				
				FileMethodCounter++;
				
				if(++MethodCounter >=NoOfMethodsToPrint){
					FlagDone = true;
					break;
				}
				
				if(FileMethodCounter >= MethodsPerFile){ // Methods allowed per file is obtained
					FileMethodCounter = 0;
					output += "\t"+"#endregion\n";
					output +="\n"+ "}"+"\n"+"//$endcategory"+ args[0];
					
					fileOut.write(output); // Finally writes to output file.
					
					//Close the output stream
				    fileOut.close();
				    
				    // Create yet another file with different filename
					fstream = new FileWriter(args[1]+ Integer.toString(FileCounter)+".cs");
					fileOut = new BufferedWriter(fstream);
					 
					output ="";
					output += "//$category "+args[0]+"\n\n";
					output += "//$benchmarkclass"+"\n";
					output += "public class "+ args[1]+Integer.toString(FileCounter)+"\n"+"{"+"\n";
					output += "\n\n\t#region "+m.name +"\n";
					FileCounter++;
				    
				}
			}
			output +="\t"+"#endregion\n";
			if(FlagDone)
				break;
			
		}
		
		// Methods involving combination (&&, || etc.) in the condition
		/*
		 * 					m
		 * 			o				n
		 * 		q		z		p		z	
		 * 	x		y		x		y	
		 */
		
		/*
		 *  This is for left sub-AST of the tree.
		 *  Otherwise it root node (m) will have duplicate sub-ASTs in both of its left and right child
		 */
		
		RepositoryOfNodes reposForLeft = new RepositoryOfNodes();
		
		
		//for(Node M: repos.combinationNodes){
		for(int i =0; i< repos.combinationNodes.size(); i++){
			if(FlagDone)
				break;			
			Node M = repos.combinationNodes.get(i);
			output += "\n\n\t#region "+ M.name+"\n";
			//for(Node N: repos.rootNodes){
			for(int j =0; j< repos.rootNodes.size(); j++){
				Node N = repos.rootNodes.get(j);
				N.right = repos.leafNodes.get(2); // node z
				//for(Node P: repos.internalNodes){
				for(int k =0; k< repos.internalNodes.size(); k++){
					Node P = repos.internalNodes.get(k);
					P.right = repos.leafNodes.get(1); // node y
					P.left	= repos.leafNodes.get(0); // node x
					N.left = P;
				
					M.right= N;
					//for(Node O: repos.rootNodes){
					for(int l=0; l< reposForLeft.rootNodes.size(); l++){
						Node O = reposForLeft.rootNodes.get(l);
						O.right = reposForLeft.leafNodes.get(2); //node z
						//for(Node Q: repos.internalNodes){
						for(int m =0; m< reposForLeft.internalNodes.size();m++){
							Node Q = reposForLeft.internalNodes.get(m);
							Q.right = reposForLeft.leafNodes.get(1); // node y
							Q.left = reposForLeft.leafNodes.get(0); // node x
							O.left = Q;
				
							M.left = O;
							//M.right = N;
						
							System.out.println("printing ...");
							//System.out.println(output);
						
							createFileBody(M);

							
							FileMethodCounter++;
							
							if(++MethodCounter >= NoOfMethodsToPrint){
								FlagDone = true;
								break;
							}
							
							if(FileMethodCounter >= MethodsPerFile){ // Methods allowed per file is obtained
								
								FileMethodCounter =0;
								output += "\t"+"#endregion\n";
								output +="\n"+ "}"+"\n"+"//$endcategory"+ args[0];
								
								fileOut.write(output); // Finally writes to output file.
								
								//Close the output stream
							    fileOut.close();
							    
							    // Create yet another file with different filename
								fstream = new FileWriter(ClassName+ Integer.toString(FileCounter)+".cs");
								fileOut = new BufferedWriter(fstream);
								 
								output ="";
								output += "//$category "+CategoryName+"\n\n";
								output += "//$benchmarkclass"+"\n";
								output += "public class "+ ClassName+Integer.toString(FileCounter)+"\n"+"{"+"\n";
								output += "\n\n\t#region "+M.name +"\n";
								
								FileCounter++;
							    
							}
						if(FlagDone)
							break;
						}
					if(FlagDone)
						break;
					}
				if(FlagDone)
					break;
				}
			if(FlagDone)
				break;
			}
			output +="\t"+"#endregion\n";
			if(FlagDone)
				break;
		}
		
		output +="\n"+ "}"+"\n"+"//$endcategory "+ CategoryName;
		
		fileOut.write(output); // Finally writes to output file.
		
		//System.out.println(output);
		
		//Close the output stream
	    fileOut.close();
	    
	    System.out.println("Successfully Printed");
	    
	    }catch (Exception e){//Catch exception if any
	      System.err.println("Error: " + e.getMessage());
	    }
		
				
	}

 }


