package edu.uta.cse.roops.proggen.bv32.arr.noex;

/*
 * This is exactly like 'IntArrayWithoutException.cs' roops file format
 * with only exception: it uses random value.
 *	// This is added comment to check 
 * 
 * @author ishtiaque.hussain@mavs.uta.edu (Ishtiaque Hussain)
 */


import java.io.*;
import java.util.Random;

public class IntArrNoEx {

	public IntArrNoEx(){
		
	}
	
	
	public static void main(String [] args){
		
		String output = "";
		
		//RepositoryOfNodes Nodes = new RepositoryOfNodes();	
		Random randGen = new Random();
		
		int randInt=0;
		
		try{
			 // Create file
			 FileWriter fstream = new FileWriter("IntArrayWithoutExcp.cs");
			 BufferedWriter fileOut = new BufferedWriter(fstream);
			 

			 output += "//$category roops.extended.bv32.arr.noex \n\n";
			 output += "//Author: Ishtiaque Hussain\n\n";
			 // RoopsArray.getLength
//			 output += "public class RoopsArray\n";
//			 output += "{\n\tpublic static int getLength(int []a)\n";
//			 output += "\t{\n\t\treturn a.Length;\n\t}\n}\n";
			 
			 output += "//$benchmarkclass\n";
			 output += "public class IntArrayWithoutExcp\n{\n\n";
			 
			 output += "\t#region Basic\n";
			 
			// for(Node m: Nodes.rootNodes){ // No need for other checks e.g., <= >= .. for array initialization
				 
				 output += "\t//$goals 2\n";
				 output += "\t//benchmark\n";
				 ////output += "\tpublic void ArrayInitialization"+ m.name+"()\n\t{\n";
				  output += "\tpublic void ArrayInitialization()\n\t{\n";
				 
				 randInt = randGen.nextInt(32767);
				 
				 output += "\t\tint[] a = new int["+ Integer.toString(randInt)+"];\n\n";
				 output += "\t\tif( a["+Integer.toString(randGen.nextInt(randInt))+"] !=  0)\n";
				 ////output += "\t\tif( a["+Integer.toString(randGen.nextInt(randInt))+"] "+ m.symbol +" 0)\n";
				 ////output += "\t\t{ /*$goal 0 */}\n";
				 output += "\t\t{ /*$goal 0 unreachable */}\n";
				 output += "\t\telse\n\t\t{ /*$goal 1*/}\n\t}\n\n";
							 
				 			 
			 //}
			output += "\t//$goals 3\n";
			output += "\t//benchmark\n";
			
			output += "\tpublic void ArrayInitializationVariableSize(int size)\n\t{\n";
			output += "\t\tif(size > 0)\n\t\t{\n";
			output += "\t\t\tint[] a = new int[size];\n";
			output += "\t\t\tif( a[0] != 0 )\n";
			output += "\t\t\t{ /*$goal 0 unreachable */}\n";
			output += "\t\t\telse\n\t\t\t{ /*$goal 1*/}\n\t\t}\n";
			output += "\t\telse\n\t\t{ /*$goal 2*/}\n\t}\n\n";
			
			 //ArrayInitializationVariableSizeVariableIndex(int size, int index)
			output += "\t//$goals 3\n";
			output += "\t//benchmark\n";
			output += "\tpublic void ArrayInitializationVariableSizeVariableIndex(int size, int index)\n\t{\n";
		    output += "\t\tif( (size > 0) && (index >= 0) && (index < size) )\n\t\t{\n";
		    output += "\t\t\tint[] a = new int[size];\n";
		    output += "\t\t\tif( a[index] != 0) \n";
		    output += "\t\t\t{ /*$goal 0 unreachable*/}\n";
		    output += "\t\t\telse\n\t\t\t{ /*$goal 1*/}\n\t\t}\n\t\telse\n\t\t{ /*$goal 2 */}\n\t}\n\n";
		    
		    output += "\t//$goals 2\n";
			output += "\t//benchmark\n";

			// public void DirectWriteFollowedByDirectRead(int v)
			output += "\tpublic void DirectWriteFollowedByDirectRead(int v)\n\t{\n";
			output += "\t\tint[] a = new int["+ Integer.toString(randInt = randGen.nextInt(32767))+"];\n";
			output += "\t\ta["+ Integer.toString(randInt= randGen.nextInt(randInt)) +"] = v;\n";
			output += "\t\tif( a["+ Integer.toString(randInt)+ "] == "+ randGen.nextInt(32767)+" )\n";
			output += "\t\t{ /*$goal 0 */}\n";
			output += "\t\telse\n\t\t{ /*$goal 1*/}\n\t}\n\n";
			
			
			// public void IndirectWriteFollowedByIndirectRead(int i, int j, int v)
			output += "\t//$goals 3\n";
			output += "\t//benchmark\n";
			output += "\tpublic void InDirectWriteFollowedByIndirectRead(int i, int j, int v)\n\t{\n";
			String temp = Integer.toString(randInt = randGen.nextInt(32767));
			output += "\t\tint[] a = new int[" + temp+ "];\n";
			output += "\t\tif( (i >= 0) && (i< "+ temp+ " ) && (j >= 0) && (j< "+ temp+") )\n\t\t{\n";
			output += "\t\t\ta[i] = v;\n";
			output += "\t\t\tif( a[j] == "+ Integer.toString(randGen.nextInt(32767))+")\n";
			output += "\t\t\t{ /*$goal 0*/}\n\t\t\telse\n\t\t\t{ /*$goal 1*/}\n\t\t}\n";
			output += "\t\telse\n\t\t{ /*$goal 2*/}\n\t}\n\n";
				
			
			// Region Big
			
			output += "\t#endregion\n\n\t#region Big\n";
			output += "\t//$goals 3\n\t//$benchmark\n";
			output += "\tpublic void BigArray( int i, int j)\n\t{\n";
			temp = Integer.toString(randInt = randGen.nextInt(32767));
			output += "\t\tif( (i >= 0) && (i < "+temp+") && (j >= 0) && (j < "+temp+") )\n\t\t{\n";
			output += "\t\t\tint[] a = new int ["+temp+"];\n";
			temp = Integer.toString(randInt = randGen.nextInt(32767));
			output += "\t\t\ta[i] = "+ temp+";\n";
			output += "\t\t\tif(a[j] == "+temp+")\n\t\t\t{/*$goal 0*/}\n\t\t\telse\n\t\t\t{ /*$goal 1*/}\n\t\t}\n";
			output += "\t\telse\n\t\t{/*$goal 2*/}\n\t}\n\n";
		  
		    output += "\t//$goals 2\n\t//$benchmark\n";
		    output += "\tpublic void BigArrayManyUpdates1(int k)\n\t{\n";
		    temp = Integer.toString(randGen.nextInt(32767));
		    output += "\t\tint[] a = new int["+temp+"];\n";
		    output += "\t\tfor( int i = 0; i < RoopsArray.getLength(a); i++)\n";
		    output += "\t\t\ta[i] = i;\n";
		    output += "\t\tif( (k >= 0) && k < RoopsArray.getLength(a) && (a[k] == " + Integer.toString(randGen.nextInt(32767))+") )\n";
		    output += "\t\t{/*$goal 0*/}\n\t\telse\n\t\t{ /*$goal 1*/}\n\t}\n\n";
		    
		    output += "\t//$goals 2\n\t//$benchmark\n";
		    output += "\tpublic void BigArrayManyUpdates2(int j, int k)\n\t{\n";
		    temp = Integer.toString(randGen.nextInt(32767));
		    output += "\t\tint[] a = new int["+ temp +"];\n";
		    output += "\t\tfor( int i=0; i < RoopsArray.getLength(a); i++)\n";
		    output += "\t\t\ta[i] = ( i == (j - "+ Integer.toString(randGen.nextInt(32767))+ ") ? 1 : 2);\n ";
		    output += "\t\tif( (k >= 0) && (k < "+ temp+ ") && (a[k] == 1) )\n";
		    output += "\t\t{ /*$goal 0 */}\n\t\telse\n\t\t{ /*$goal 1 */}\n\t}\n\n\t#endregion\n";

		    

		    	
			 
			// output += "\t#endregion\n";
			 output += "}\n//$endcategory roops.extended.bv32.arr.noex\n";
			 
			 fileOut.write(output);
			 fileOut.close();
			System.out.println("File Successfuuly written!!");
			 
		}
		catch(Exception e){
			System.err.println("Error!!: "+ e.getMessage());
			
		}
	}
	
}
