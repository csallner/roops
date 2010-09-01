/*****************************************************************************
 * University of Illinois/NCSA Open Source License
 *
 * Copyright (c) 2010 Rohan Sharma
 * All rights reserved.
 *
 * Developed by:  Rohan Sharma <sharma.rohan1990@gmail.com>
 *                University of Illinois at Urbana-Champaign
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal with the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 *  * Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimers.
 *  * Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimers in the
 *    documentation and/or other materials provided with the distribution.
 *  * Neither the names of Rohan Sharma, University of Illinois at Urbana-
 *    Champaign, nor the names of its contributors may be used to endorse
 *    or promote products derived from this Software without specific prior
 *    written permission.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL
 * THE CONTRIBUTORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 *****************************************************************************/

/**
 * ArrayList implementation based on Java 5.0 specification.
 * 
 * @author Rohan Sharma <sharma.rohan190@gmail.com>
 * 
 */

//$category roops.collections
public class RoopsList<T> {

    private Object[] arr;
    private int len;
    private int arrlen;

    public RoopsList(RoopsList<T> rhs) {
        len = rhs.len;
        arrlen = rhs.arrlen;

        for (int i = 0; i < len; i++)
            arr[i] = rhs.arr[i];
    }

    public RoopsList() {
        clear();
    }

    public void clear() {
        arr = null;
        len = 0;
        arrlen = 0;
    }

    public void add(int index, T elem) {
        len++;

        if (len == arrlen)
            resize(len * 2);

        if (index >= len)
            return;

        for (int i = len - 1; i > index; i--)
            arr[i] = arr[i - 1];

        arr[index] = elem;
    }

    public boolean add(T elem) {
        add(len, elem);
        return true;
    }

    public boolean remove(int index) {
        if (index >= len)
            return false;

        for (int i = index; i < len - 1; i++)
            arr[i] = arr[i + 1];

        len--;

        if (len * 4 <= arrlen)
            resize(arrlen / 2);

        return true;
    }

    public boolean remove(T elem) {
        return remove(indexOf(elem));
    }

    public T get(int index) {
        return index < len ? (T) arr[index] : null;
    }

    public T set(int index, T elem) {
        if (index < len) {
            arr[index] = elem;
            return elem;
        }
        return null;
    }

    public int indexOf(T elem) {
        for (int i = 0; i < len; i++)
            if (arr[i] == elem)
                return i;
        return len;
    }

    public boolean isEmpty() {
        return len == 0;
    }

    public boolean contains(T elem) {
        return indexOf(elem) != len;
    }

    public int size() {
        return len;
    }

    private void resize(int n) {
        Object[] narr = new Object[n];

        if (n < arrlen)
            arrlen = n;
            
        for (int i = 0; i < arrlen; i++)
            narr[i] = arr[i];
        
        arr = new Object[n];
        
        for(int i = 0; i < arrlen; i++)
            arr[i] = narr[i];
            
        arrlen = n;
    }

}
//$endcategory roops.util

