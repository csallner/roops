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
 * A map implementation based on the Java 5.0 specification of the class
 * TreeMap. An adaptation of a red black tree implementation described in
 * "Introduction to Algorithms" by Cormen, Leiserson, Rivest, and Stein. The
 * following code can be used for testing research purposes.
 * 
 * @author Rohan Sharma <sharma.rohan1990@gmail.com>
 * 
 */

//$category roops.collections
public class RoopsMap<K, V> {
    private int size;
    private Comparator<K> comp;
    private /*@ nullable @*/  RoopsList<Entry<K, V>> entries;

    private class Node {
        K key;
        V value;
        Node parent;
        Node left, right;
        boolean color;
    }

    private Node root;

    public RoopsMap(Comparator<K> comp) {
        this.comp = comp;
    }

    public RoopsMap(/*@ nullable @*/ RoopsMap<K, V> rhs) {
        this.root = rhs.root;
    }

    public boolean isEmpty() {
        return root == null;
    }

    public boolean containsKey(K key) {
        return findNode(key) != null;
    }

    public /*@ nullable @*/ V get(K key) {
        Node x = findNode(key);
        return x == null ? null : x.value;
    }

    public void put(K key, V value) {
        Node x = new Node();
        x.key = key;
        x.value = value;
        treeInsert(x);
        size++;
    }

    public /*@ nullable @*/ V remove(K key) {
        Node ret = treeDelete(findNode(key));
        if (ret == null)
            return null;
        size--;
        return ret.value;
    }

    public int getSize() {
        return size;
    }

    public /*@ nullable @*/ RoopsList<Entry<K, V>> entryList() {
        entries.clear();
        addEntries(root);
        return entries;
    }

    private boolean getColor(Node x) {
        // null nodes are colored black
        return x == null ? true : x.color;
    }

    private Node findNode(K key) {
        Node cur = root;
        while (cur != null && comp.compare(key, cur.key) != 0)
            if (comp.compare(key, cur.key) < 0)
                cur = cur.left;
            else
                cur = cur.right;
        return cur;
    }

    private void leftRotate(Node x) {
        Node y = x.right;
        x.right = y.left;
        if (y.left != null)
            y.left.parent = x;
        y.parent = x.parent;
        if (x.parent == null)
            root = y;
        else if (x == x.parent.left)
            x.parent.left = y;
        else
            x.parent.right = y;
        y.left = x;
        x.parent = y;
    }

    private void rightRotate(Node x) {
        Node y = x.left;
        x.left = y.right;
        if (y.right != null)
            y.right.parent = x;
        y.parent = x.parent;
        if (x.parent == null)
            root = y;
        else if (x == x.parent.right)
            x.parent.right = y;
        else
            x.parent.left = y;
        y.right = x;
        x.parent = y;
    }

    private void treeInsert(Node z) {
        Node y = null;
        Node x = root;
        while (x != null) {
            y = x;
            if (comp.compare(z.key, x.key) < 0)
                x = x.left;
            else if (comp.compare(z.key, x.key) == 0) {
                x.value = z.value;
                size--;
                return;
            } else
                x = x.right;
        }
        z.parent = y;
        if (y == null)
            root = z;
        else if (comp.compare(z.key, y.key) < 0)
            y.left = z;
        else
            y.right = z;
        z.left = null;
        z.right = null;
        z.color = false;

        treeInsertFix(z);
    }

    private void treeInsertFix(Node z) {
        while (getColor(z.parent) == false) {
            if (z.parent == z.parent.parent.left) {
                Node y = z.parent.parent.right;
                if (getColor(y) == false) {
                    z.parent.color = true;
                    y.color = true;
                    z.parent.parent.color = false;
                    z = z.parent.parent;
                } else {
                    if (z == z.parent.right) {
                        z = z.parent;
                        leftRotate(z);
                    }
                    z.parent.color = true;
                    z.parent.parent.color = false;
                    rightRotate(z.parent.parent);
                }
            } else {
                Node y = z.parent.parent.left;
                if (getColor(y) == false) {
                    z.parent.color = true;
                    y.color = true;
                    z.parent.parent.color = false;
                    z = z.parent.parent;
                } else {
                    if (z == z.parent.left) {
                        z = z.parent;
                        rightRotate(z);
                    }
                    z.parent.color = true;
                    z.parent.parent.color = false;
                    leftRotate(z.parent.parent);
                }
            }
        }
        root.color = true;
    }

    private Node treeDelete(Node z) {
        if (z == null)
            return null;
        Node x, y;
        if (z.left == null || z.right == null)
            y = z;
        else
            y = getIOS(z);
        if (y.left != null)
            x = y.left;
        else
            x = y.right;
        if (x != null)
            x.parent = y.parent;
        if (y.parent == null)
            root = x;
        else if (y == y.parent.left)
            y.parent.left = x;
        else
            y.parent.right = x;
        if (y != z) {
            z.key = y.key;
            z.value = y.value;
        }
        if (getColor(y) == true)
            if (x == null)
                treeDeleteFix(y);
            else
                treeDeleteFix(x);
        return y;
    }

    private void treeDeleteFix(Node x) {
        while (x.parent != null && getColor(x) == true) {
            if (x == x.parent.left || x.parent.left == null) {
                Node w = x.parent.right;
                if (w == null)
                    return;
                if (getColor(w) == false) {
                    w.color = true;
                    x.parent.color = false;
                    leftRotate(x.parent);
                    w = x.parent.right;
                }
                if (getColor(w.left) == true && getColor(w.right) == true) {
                    w.color = false;
                    x = x.parent;
                } else {
                    if (getColor(w.right) == true) {
                        w.left.color = true;
                        rightRotate(w);
                        w = x.parent.right;
                    }
                    w.color = x.parent.color;
                    x.parent.color = true;
                    if (w.right != null)
                        w.right.color = true;
                    leftRotate(x.parent);
                    x = root;
                }
            } else {
                Node w = x.parent.left;
                if (w == null)
                    return;
                if (getColor(w) == false) {
                    w.color = true;
                    x.parent.color = false;
                    rightRotate(x.parent);
                    w = x.parent.left;
                }
                if (getColor(w.right) == true && getColor(w.left) == true) {
                    w.color = false;
                    x = x.parent;
                } else {
                    if (getColor(w.left) == true) {
                        w.right.color = true;
                        leftRotate(w);
                        w = x.parent.left;
                    }
                    w.color = x.parent.color;
                    x.parent.color = true;
                    if (w.left != null)
                        w.left.color = true;
                    rightRotate(x.parent);
                    x = root;
                }
            }
        }
        x.color = true;
    }

    private Node getIOS(Node z) {
        Node x, y = null;
        x = z.right;
        while (x != null) {
            y = x;
            x = x.left;
        }
        return y;
    }

    private void addEntries(Node x) {
        if (x == null)
            return;
        entries.add(new Entry<K, V>(x.key, x.value));
        addEntries(x.left);
        addEntries(x.right);
    }
}
//$endcategory roops.util

