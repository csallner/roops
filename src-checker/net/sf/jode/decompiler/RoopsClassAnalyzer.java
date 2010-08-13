package net.sf.jode.decompiler;

import java.io.IOException;

import net.sf.jode.bytecode.BasicBlocks;
import net.sf.jode.bytecode.Block;
import net.sf.jode.bytecode.ClassFormatException;
import net.sf.jode.bytecode.ClassInfo;
import net.sf.jode.bytecode.Instruction;
import net.sf.jode.bytecode.MethodInfo;
import net.sf.jode.bytecode.Reference;
import net.sf.jode.decompiler.ClassAnalyzer;
import net.sf.jode.decompiler.ImportHandler;

/**
 * 
 * @author csallner@uta.edu (Christoph Csallner)
 */
public class RoopsClassAnalyzer extends ClassAnalyzer {
	
	/**
	 * Constructor
	 */
	public RoopsClassAnalyzer(ClassInfo clazz, ImportHandler imports)
	throws ClassFormatException, IOException
  {
		super(null, clazz, imports);
  }

	protected String getFullName(MethodInfo mInfo){
		return clazz.getName()+"."+mInfo.getName()+mInfo.getType();
	}
	
	/**
	 * Sift through all instructions of all declared methods
	 * 
	 * @return if the wrapped class satisfies the Roops language restrictions.
	 */
	public boolean checkRoopsRules()
	{
		if (clazz==null)
			return false;
		
		MethodInfo[] methodInfos = clazz.getMethods();		
		if (methodInfos==null)
			return true;		
		
		for (MethodInfo methodInfo: methodInfos) 
		{
			BasicBlocks bb = methodInfo.getBasicBlocks();
			Block[] blocks = bb.getBlocks();
			for (Block block: blocks)
			{
				Instruction[] instructions = block.getInstructions();
				for (Instruction instruction: instructions)
				{
					switch (instruction.getOpcode()) {
					
					/* Check for call to <method_name, parameter_types>.
					 * Ignore the static receiver type, as the dynamic receiver type
					 * is not guaranteed to declare the same methods, see:
					 * http://java.sun.com/docs/books/jvms/second_edition/html/Instructions2.doc6.html#invokevirtual
					 * http://java.sun.com/docs/books/jls/second_edition/html/binaryComp.doc.html#44872 */
					case Opcodes.opc_invokespecial:
					case Opcodes.opc_invokevirtual:
						Reference ref = instruction.getReference();						
					  if ("equals".equals(ref.getName()) && "(Ljava/lang/Object;)Z".equals(ref.getType()))
					  	System.out.println("found call to equals(Object) in method: "+getFullName(methodInfo));
					  else if ("hashCode".equals(ref.getName()) && "()I".equals(ref.getType()))
					  	System.out.println("found call to hashCode() in method: "+getFullName(methodInfo));
						break;

					default:
						break;
					} 
				}
			}
			
		}
		
		return true;
	}
}
