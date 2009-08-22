package edu.uta.cse.roops.proggen.bv32.ast;

/*
 * The Node for the AST
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 *  
 */

public class Node {
	public Node left;
	public Node right;
	public String symbol;
	public String name;
	public String type;
	
	/* Constructor
	 * 
	 */
	
	public Node(String sym, String nm){
		
		symbol = sym;
		left = null;
		right = null;
		name = nm;
		type ="";
	}
	public Node(String sym, Node lft, Node rt, String nm){
		symbol = sym;
		left = lft;
		right = rt;
		name = nm;
		type = "";
	}
	public Node(String sym, String nm, String typ){
		symbol = sym;
		left = null;
		right = null;
		name = nm;
		type = typ;
		
		
	}


}
