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
		
		boolean okay = true;
		
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
					  {
					  	System.out.println("found call to equals(Object) in method: "+getFullName(methodInfo));
					  	okay = false;
					  }
					  else if ("hashCode".equals(ref.getName()) && "()I".equals(ref.getType()))
					  {
					  	System.out.println("found call to hashCode() in method: "+getFullName(methodInfo));
					  	okay = false;
					  }
						break;
						
						
						/* Do not allow any operation on double values */
					case Opcodes.opc_d2f:
					case Opcodes.opc_d2i:
					case Opcodes.opc_d2l:
					case Opcodes.opc_dadd:
					case Opcodes.opc_daload:
					case Opcodes.opc_dastore:
					case Opcodes.opc_dcmpg:
					case Opcodes.opc_dcmpl:
					case Opcodes.opc_dconst_0:
					case Opcodes.opc_dconst_1:
					case Opcodes.opc_ddiv:
					case Opcodes.opc_dload:
					case Opcodes.opc_dload_0:
					case Opcodes.opc_dload_1:
					case Opcodes.opc_dload_2:
					case Opcodes.opc_dload_3:
					case Opcodes.opc_dmul:
					case Opcodes.opc_dneg:
					case Opcodes.opc_drem:
					case Opcodes.opc_dreturn:
					case Opcodes.opc_dstore:
					case Opcodes.opc_dstore_0:
					case Opcodes.opc_dstore_1:
					case Opcodes.opc_dstore_2:
					case Opcodes.opc_dstore_3:
					case Opcodes.opc_dsub:
						System.out.println("found operation on double in method: "+getFullName(methodInfo));
						okay = false;
						break;	
						
					/* Do not allow any operation on float values */
					case Opcodes.opc_f2d:
					case Opcodes.opc_f2i:
					case Opcodes.opc_f2l:
					case Opcodes.opc_fadd:
					case Opcodes.opc_faload:
					case Opcodes.opc_fastore:
					case Opcodes.opc_fcmpg:
					case Opcodes.opc_fcmpl:
					case Opcodes.opc_fconst_0:
					case Opcodes.opc_fconst_1:
					case Opcodes.opc_fconst_2:
					case Opcodes.opc_fdiv:
					case Opcodes.opc_fload:
					case Opcodes.opc_fload_0:
					case Opcodes.opc_fload_1:
					case Opcodes.opc_fload_2:
					case Opcodes.opc_fload_3:
					case Opcodes.opc_fmul:
					case Opcodes.opc_fneg:
					case Opcodes.opc_frem:
					case Opcodes.opc_freturn:
					case Opcodes.opc_fstore:
					case Opcodes.opc_fstore_0:
					case Opcodes.opc_fstore_1:
					case Opcodes.opc_fstore_2:
					case Opcodes.opc_fstore_3:
					case Opcodes.opc_fsub:
						System.out.println("found operation on float in method: "+getFullName(methodInfo));
						okay = false;
						break;					

					/* Do not allow any operation on long values */
					case Opcodes.opc_l2d:
					case Opcodes.opc_l2f:
					case Opcodes.opc_l2i:
					case Opcodes.opc_ladd:
					case Opcodes.opc_laload:
					case Opcodes.opc_land:
					case Opcodes.opc_lastore:
					case Opcodes.opc_lcmp:
					case Opcodes.opc_lconst_0:
					case Opcodes.opc_lconst_1:
					case Opcodes.opc_ldiv:
					case Opcodes.opc_lload:
					case Opcodes.opc_lload_0:
					case Opcodes.opc_lload_1:
					case Opcodes.opc_lload_2:
					case Opcodes.opc_lload_3:
					case Opcodes.opc_lmul:
					case Opcodes.opc_lneg:
					case Opcodes.opc_lor:
					case Opcodes.opc_lrem:
					case Opcodes.opc_lreturn:
					case Opcodes.opc_lshl:
					case Opcodes.opc_lshr:
					case Opcodes.opc_lstore:
					case Opcodes.opc_lstore_0:
					case Opcodes.opc_lstore_1:
					case Opcodes.opc_lstore_2:
					case Opcodes.opc_lstore_3:
					case Opcodes.opc_lsub:
					case Opcodes.opc_lushr:
					case Opcodes.opc_lxor:
						System.out.println("found operation on long in method: "+getFullName(methodInfo));
						okay = false;
						break;
						
					default:
						break;
					} 
				}
			}
			
		}
		
		return okay;
	}
}
