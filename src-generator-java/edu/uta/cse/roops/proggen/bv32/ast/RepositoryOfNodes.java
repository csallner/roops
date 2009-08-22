package edu.uta.cse.roops.proggen.bv32.ast;

import java.util.*;


/*
 * Repository of different kind of AST nodes
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 */

public class RepositoryOfNodes {

	public ArrayList<Node> internalNodes = new ArrayList<Node>();
	public ArrayList<Node> rootNodes = new ArrayList<Node>();
	public ArrayList<Node> leafNodes = new ArrayList<Node>();
	public ArrayList<Node> combinationNodes = new ArrayList<Node>();
	
	//internalNode =(new Node("+", "Plus"));
	public RepositoryOfNodes(){
		
		internalNodes.add(new Node("+", "Plus"));
		internalNodes.add(new Node("-", "Minus"));
		internalNodes.add(new Node("*", "MultiplyBy"));
		internalNodes.add(new Node("/", "DivideBy"));
		internalNodes.add(new Node("%", "Modulo"));
		
		rootNodes.add(new Node("==","Equlas"));
		rootNodes.add(new Node("!=","Unequlas"));
		rootNodes.add(new Node("<","LessThan"));
		rootNodes.add(new Node("<=","LessEqualThan"));
		rootNodes.add(new Node(">","GreaterThan"));
		rootNodes.add(new Node(">=","GreatherEqualThan"));
		
		combinationNodes.add(new Node("&&", "AND"));
		combinationNodes.add(new Node("||", "OR"));
		combinationNodes.add(new Node("&", "And_next"));
		combinationNodes.add(new Node("|", "Or_next"));
		//combinationNodes.add(new Node("&&", "And_Shorts"));
		
		
		leafNodes.add(new Node("x","X"));
		leafNodes.add(new Node("y","Y"));
		leafNodes.add(new Node("z","Z"));
		
		
		
		
		
	}
	
}
