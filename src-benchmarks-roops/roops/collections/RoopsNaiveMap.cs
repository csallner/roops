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
 * A naive implementation of the Java Map container. 
 * Temporary workaround for objects with no Comparators.
 *
 * @author Rohan Sharma <sharma.rohan1990@gmail.com>
 * 
 */

//$category roops.collections
public class RoopsNaiveMap<K, V> {
    private /*@ nullable @*/ RoopsList<K> keys;
    private /*@ nullable @*/ RoopsList<V> vals;
    private /*@ nullable @*/ RoopsList<Entry<K, V>> entries;
    private int size;

    public RoopsNaiveMap() {
        keys = new RoopsList<K>();
        vals = new RoopsList<V>();
        entries = new RoopsList<Entry<K, V>>();
    }

    public boolean isEmpty() {
        return entries.isEmpty();
    }

    public boolean containsKey(K key) {
        return keys.contains(key);
    }

    public V get(K key) {
        return vals.get(getIndex(key));
    }

    public void put(K key, /*@ nullable @*/ V value) {
        keys.add(key);
        vals.add(value);
        entries.add(new Entry<K, V>(key, value));
        size++;
    }

    public V remove(K key) {
        int index = getIndex(key);
        V value = vals.get(index);

        keys.remove(index);
        vals.remove(index);
        entries.remove(index);

        return value;
    }

    public /*@ nullable @*/ RoopsList<V> values() {
        return vals;
    }

    public int getSize() {
        return size;
    }

    public /*@ nullable @*/ RoopsList<Entry<K, V>> entryList() {
        return entries;
    }

    private int getIndex(K key) {
        return keys.indexOf(key);
    }
}
//$endcategory roops.util

