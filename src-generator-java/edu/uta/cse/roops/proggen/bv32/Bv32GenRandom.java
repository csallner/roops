package edu.uta.cse.roops.proggen.bv32;

import java.io.*;
import java.util.Random;

import edu.uta.cse.roops.proggen.bv32.ast.Node;
import edu.uta.cse.roops.proggen.bv32.ast.Tree;

/*
 * This class generates user defined number of methods involving deep ASTs.
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 * 
 */




public class Bv32GenRandom {
	
		//4 types of Boolean Nodes
		static String [][]boolNode = {
				{"&&","AND"},
				{"||","OR"},
				{"&" ,"AND_Next"},
				{"|" ,"OR_Next"}
				};  				
		
		// 6 types of Equality Nodes
		static String [][] equalityNode = {
				{"==","Equals"},
				{"!=","Unequals"},
				{ ">","GreaterThan"},
				{">=","GreaterEqualThan"},
				{ "<","LessThan"},{ "<=","LessEqualThan"}
				};
		
		// 5 types of Operator nodes
		static String [][] opNode = {
				{"+","Plus"},
				{"-","Minus"},
				{"*","MultiplyBy"},
				{"/","DivideBy"},
				{"%","Modulo"}
				};				
		
		
		
		static Random randGen = new Random();
		static int NoOfVars = 0;
		static int Probability = 50; //
	
		
/*
 * Depending on the node m, it keeps creating(or expanding) the tree.
 * 
 * boolNodes can have boolNodes or equalityNodes as its children, but not operator nodes (+, -, *, etc.)
 * 
 * 					&&
 * 			+				-
 * 		5		6		3		3
 * 
 * ==> {(5+6) && (3-3)} is not a valid condition
 * 	
 * equlityNode can have opNodes or literals (which is the terminal) as children; but not boolNodes
 * 
 * 		 		
 * literals could be either variables or concrete values.
 * 
 * Java supports 256 parameters. So, it creates concrete values as leaf node when no. of generated
 * variables cross 255.
 * 
 */	
	
	public  Node CreateTree(Node m){
		
		if(m.type == "bool"){
			// for left child
			int bool_eq = randGen.nextInt(10);
			if(bool_eq < 4){ // again bool node
				int bool = randGen.nextInt(4);
				m.left = CreateTree(new Node(boolNode[bool][0], boolNode[bool][1], "bool"));
			}
			else{ // Equality Node
				int eq = randGen.nextInt(6); // any of the the equality Nodes 
				m.left = CreateTree(new Node(equalityNode[eq][0],equalityNode[eq][0], "equality"));
			}
			
			// for right child
			bool_eq = randGen.nextInt(10);
			if(bool_eq < 4){ // again bool node
				int bool = randGen.nextInt(4);
				m.right = CreateTree(new Node(boolNode[bool][0], boolNode[bool][1], "bool"));
			}
			else{ // Equality Node
				int eq = randGen.nextInt(6); // any of the the equality Nodes 
				m.right = CreateTree(new Node(equalityNode[eq][0],equalityNode[eq][0], "equality"));
			}
			
			
		}
		else if(m.type == "equality"){
			// left child
			
			int op_lit = randGen.nextInt(10);	
			if(op_lit < 4){ // Operator Node
				int op = randGen.nextInt(5);
				m.left = CreateTree(new Node(opNode[op][0], opNode[op][1], "op"));				
			}
			else { //Variable Or literal
				if(NoOfVars < 255 && (randGen.nextInt(100) < Probability) ) {
					m.left = CreateTree(new Node("x"+Integer.toString(++NoOfVars),"Variable","var"));
				}				
				else{ //literal
					int lit = randGen.nextInt(32767);
					m.left = CreateTree(new Node(Integer.toString(lit), "Literal", "lit"));				
				}
			}
			
			// right child
			op_lit = randGen.nextInt(10);
			if(op_lit < 4){ // Operator Node
				int op = randGen.nextInt(5);
				m.right = CreateTree(new Node(opNode[op][0], opNode[op][1], "op"));				
			}
			else { //Variable Or literal
				
				if(NoOfVars < 255 && (randGen.nextInt(100) < Probability) ) {
					m.right = CreateTree(new Node("x"+Integer.toString(++NoOfVars),"Variable","var"));
				}
				else{ //literal
					int lit = randGen.nextInt(32767);
					m.right = CreateTree(new Node(Integer.toString(lit), "Literal", "lit"));				
				} 
			}
		}
		else if(m.type == "op"){
			
			// left child
			int op_lit = randGen.nextInt(10);
			if(op_lit < 4){ // Operator Node
				int op = randGen.nextInt(5);
				m.left = CreateTree(new Node(opNode[op][0], opNode[op][1], "op"));				
			}
			else { //Variable Or literal
				
				if(NoOfVars < 255 && (randGen.nextInt(100) < Probability) ) {
					m.left = CreateTree(new Node("x"+Integer.toString(++NoOfVars),"Variable","var"));
				}
				else{ //literal
					int lit = randGen.nextInt(32767);
					m.left = CreateTree(new Node(Integer.toString(lit), "Literal", "lit"));				
				}
			}
			
			// right child
			op_lit = randGen.nextInt(10);
			if(op_lit < 4){ // Operator Node
				int op = randGen.nextInt(5);
				m.right = CreateTree(new Node(opNode[op][0], opNode[op][1], "op"));				
			}
			else { //Variable Or literal
				if(NoOfVars < 255 && (randGen.nextInt(100) < Probability) ) {
					m.right = CreateTree(new Node("x"+Integer.toString(++NoOfVars),"Variable","var"));
				}
				else{ //literal
					int lit = randGen.nextInt(32767);
					m.right = CreateTree(new Node(Integer.toString(lit), "Literal", "lit"));				
				}
			}
			
		}
		else{
			
			// No more node: Literal node reached.
		}
		
		return m;
		
	}
		
	/*
	 *  Step 1: Choose Root of the Tree:
	 *  	Root node can be either of Boolean type (&&, ||, ...) or Equality type( ==, !=, <=, ...)
	 *  Use CreateTee() to build the Tree:
	 *  	CrateTree(Node) recursively creates the whole tree.
	 */
	
	public static void main(String [] args){
		
		System.out.println("Usage: CategoryName NO_OF_METHODS ProbabiltyOfVaribaleNodeInLeaf\n");
				
	
		String categoryName ="roops.extended.bv32";
		int MethodLimit = 200;
		
		if(args.length == 3){			
						
			categoryName = args[0];
			MethodLimit = Integer.parseInt(args[1]);
			
			//Probability of occurring a variable;
			Probability = Integer.parseInt(args[2]);
		}
		
		Bv32GenRandom obj = new Bv32GenRandom();
				
		try{
			// Create file
			FileWriter fstream = new FileWriter("DeepASTrandom.cs");
			BufferedWriter fileOut = new BufferedWriter(fstream);
			
			String output ="";
			output += "//$category "+ categoryName  +"\n\n";
			output += "// Author: Ishtiaque Hussain\n\n ";
			output += "//$benchmarkclass"+"\n";
			output += "public class "+ "DeepASTrandom"+"\n"+"{"+"\n";
			
			Node root = null;
			
			
			int bool_eq;
			int methodCounter = 1;	// Used to number the methods in file printed so far.
			
			
			for(int m = 0; m < MethodLimit; m++ ){
				
				NoOfVars = 0;
				
				bool_eq = randGen.nextInt(10);		// 0= Bool; 1= Equality;						
				
				if(bool_eq < 4 ){
					int bool = randGen.nextInt(4);
					root = obj.CreateTree( new Node(boolNode[bool][0], boolNode[bool][1], "bool"));
				}
				else{
					int eq = randGen.nextInt(6);
					root = obj.CreateTree(new Node(equalityNode[eq][0], equalityNode[eq][1],"equality"));
				}
				
				
				
				Tree  tr = new Tree(root);
				//System.out.println(tr.printCondition(tr.Root));
				
				
						 
				 output += "\t//$goals 2"+"\n";
				 output += "\t//$benchmark" +"\n";
				 output += "\tpublic void " + "Method"+ Integer.toString(methodCounter++)+"(";
				 for(int i=1; i<= NoOfVars; i++){
					 if(i != 1){
						 output += ", int x"+Integer.toString(i);
					 }
					 else
						 output+=  "int x"+Integer.toString(i);
				 }
				 
				 output +=")\n";
				 output += "\t{"+"\n";
				 output += "\t\tif"+tr.printCondition(tr.Root)+"\n";
				 output += "\t\t\t{ /*$goal 0*/}\n";
				 output += "\t\telse\n";
				 output += "\t\t\t{ /*$goal 1*/}\n\t}\n\n\n";
			}
			 output += "\n}\n\n\n//$endcategory "+categoryName;
			 fileOut.write(output); // Finally writes to output file.
			 fileOut.close();
			 System.out.println("Printed successfully");
				 
		}
		catch(Exception e){
			System.err.print(e.getMessage());
		}
			
		
		
	}

}
