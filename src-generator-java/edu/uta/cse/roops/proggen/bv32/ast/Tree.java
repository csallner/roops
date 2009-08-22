package edu.uta.cse.roops.proggen.bv32.ast;


/*
 * The AST for condition
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 *  
 */
public class Tree {
	
	public Node Root;
	
	public Tree(Node root){
		Root = root;
	}
	
	//Inorder AST traversal to get the Condition
	public String printCondition(Node node){
		String cond ="";
		
		if(node.left != null){
			cond += "("+printCondition(node.left);
		}
		cond += " "+node.symbol;
		if(node.right != null){
			cond +=  printCondition(node.right)+")";
		}
		
		//cond +="\n";
		return cond;
	}

	// Inorder AST traversal for finding the MethodName
	public String MethodName(Node node){
		String mthd ="";
		
		if(node.left != null){
			mthd += MethodName(node.left);
		}
		mthd +=node.name;
		if(node.right != null){
			mthd += MethodName(node.right);
		}
		
		return mthd;
		
	}
	
	
}
