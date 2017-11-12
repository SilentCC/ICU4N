﻿using ICU4N.Impl;
using ICU4N.Support.IO;
using System;
using System.Text;

namespace ICU4N.Util
{
    /// <summary>
    /// A simple utility class to wrap a byte array.
    /// <para/>
    /// Generally passed as an argument object into a method. The method takes
    /// responsibility of writing into the internal byte array and increasing its
    /// size when necessary.
    /// </summary>
    /// <author>syn wee</author>
    /// <stable>ICU 2.8</stable>
    public class ByteArrayWrapper : IComparable<ByteArrayWrapper>
    {
        // public data member ------------------------------------------------

        /// <summary>
        /// Internal byte array.
        /// </summary>
        /// <stable>ICU 2.8</stable>
        public byte[] bytes; // ICU4N TODO: API Make this into GetBytes() ? We really shouldn't expose arrays like this

        /// <summary>
        /// Size of the internal byte array used. 
        /// Different from bytes.length, size will be &lt;= bytes.length.
        /// Semantics of Count is similar to java.util.Vector.size().
        /// </summary>
        /// <stable>ICU 2.8</stable>
        public int Count { get; set; }

        // public constructor ------------------------------------------------

        /// <summary>
        /// Construct a new ByteArrayWrapper with no data.
        /// </summary>
        /// <stable>ICU 2.8</stable>
        public ByteArrayWrapper()
        {
            // leave bytes null, don't allocate twice
        }

        /// <summary>
        /// Construct a new <see cref="ByteArrayWrapper"/> from a byte array and size. 
        /// </summary>
        /// <param name="bytesToAdopt">The byte array to adopt.</param>
        /// <param name="size">The length of valid data in the byte array.</param>
        /// <exception cref="IndexOutOfRangeException">if bytesToAdopt == null and size != 0, or
        /// size &lt; 0, or size &gt; bytesToAdopt.length.</exception>
        /// <stable>ICU 3.2</stable>
        public ByteArrayWrapper(byte[] bytesToAdopt, int size)
        {
            if ((bytesToAdopt == null && size != 0) || size < 0 || (bytesToAdopt != null && size > bytesToAdopt.Length))
            {
                throw new IndexOutOfRangeException("illegal size: " + size);
            }
            this.bytes = bytesToAdopt;
            this.Count = size;
        }

        /// <summary>
        /// Construct a new ByteArrayWrapper from the contents of a <see cref="ByteBuffer"/>.
        /// </summary>
        /// <param name="source">The <see cref="ByteBuffer"/> from which to get the data.</param>
        /// <stable>ICU 3.2</stable>
        public ByteArrayWrapper(ByteBuffer source)
        {
            Count = source.Limit;
            bytes = new byte[Count];
            source.Get(bytes, 0, Count);
        }

        /**
         * Create from ByteBuffer
         * @param byteBuffer
        public ByteArrayWrapper(ByteArrayWrapper source) {
            size = source.size;
            bytes = new byte[size];
            copyBytes(source.bytes, 0, bytes, 0, size);
        }
         */

        /**
         * create from byte buffer
         * @param src
         * @param start
         * @param limit
        public ByteArrayWrapper(byte[] src, int start, int limit) {
            size = limit - start;
            bytes = new byte[size];
            copyBytes(src, start, bytes, 0, size);
        }
         */

        // public methods ----------------------------------------------------

        /// <summary>
        /// Ensure that the internal byte array is at least of length capacity.
        /// If the byte array is null or its length is less than capacity, a new
        /// byte array of length capacity will be allocated.
        /// The contents of the array (between 0 and <see cref="Count"/>) remain unchanged.
        /// </summary>
        /// <param name="capacity">Minimum length of internal byte array.</param>
        /// <returns>This <see cref="ByteArrayWrapper"/>.</returns>
        /// <stable>ICU 3.2</stable>
        public virtual ByteArrayWrapper EnsureCapacity(int capacity)
        {
            if (bytes == null || bytes.Length < capacity)
            {
                byte[] newbytes = new byte[capacity];
                if (bytes != null)
                {
                    CopyBytes(bytes, 0, newbytes, 0, Count);
                }
                bytes = newbytes;
            }
            return this;
        }

        /// <summary>
        /// Set the internal byte array from offset 0 to count with the 
        /// contents of src from offset start to limit. If the byte array is null or its length is less than capacity, a new 
        /// byte array of length (limit - start) will be allocated.
        /// This resets the size of the internal byte array to count.
        /// </summary>
        /// <param name="src">source byte array to copy from</param>
        /// <param name="start">start offset of <paramref name="src"/> to copy from</param>
        /// <param name="count">Number of bytes to copy from <paramref name="src"/>.</param>
        /// <returns>This <see cref="ByteArrayWrapper"/>.</returns>
        /// <stable>ICU 3.2</stable>
        public ByteArrayWrapper Set(byte[] src, int start, int count)
        {
            Count = 0;
            Append(src, start, count);
            return this;
        }

        /*
        public final ByteArrayWrapper get(byte[] target, int start, int limit) 
        {
            int len = limit - start;
            if (len > size) throw new IllegalArgumentException("limit too long");
            copyBytes(bytes, 0, target, start, len);
            return this;
        }
        */

        /// <summary>
        /// Appends the internal byte array from offset size with the 
        /// contents of src from offset start to limit. This increases the size of
        /// the internal byte array to (size + count).
        /// </summary>
        /// <param name="src">source byte array to copy from.</param>
        /// <param name="start">start offset of <paramref name="src"/> to copy from.</param>
        /// <param name="count">number of bytes from <paramref name="src"/> to copy.</param>
        /// <returns>This ByteArrayWrapper.</returns>
        /// <stable>ICU 3.2</stable>
        public ByteArrayWrapper Append(byte[] src, int start, int count) // ICU4N specific - changed 3rd parameter from end to count (like in .NET)
        {
            int len = count;
            EnsureCapacity(Count + len);
            CopyBytes(src, start, bytes, Count, len);
            Count += len;
            return this;
        }

        /*
        public final ByteArrayWrapper append(ByteArrayWrapper other) 
        {
            return append(other.bytes, 0, other.size);
        }
        */

        /// <summary>
        /// Releases the internal byte array to the caller, resets the internal
        /// byte array to null and its size to 0.
        /// </summary>
        /// <returns>internal byte array.</returns>
        /// <stable>ICU 2.8</stable>
        public byte[] ReleaseBytes()
        {
            byte[] result = bytes;
            bytes = null;
            Count = 0;
            return result;
        }

        // Boilerplate ----------------------------------------------------

        /// <summary>
        /// Returns string value for debugging.
        /// </summary>
        /// <returns>ICU 3.2</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Count; ++i)
            {
                if (i != 0) result.Append(" ");
                result.Append(Utility.Hex(bytes[i] & 0xFF, 2));
            }
            return result.ToString();
        }

        /// <summary>
        /// Return true if the bytes in each wrapper are equal.
        /// </summary>
        /// <param name="other">the object to compare to.</param>
        /// <returns>true if the two objects are equal.</returns>
        /// <stable>ICU 3.2</stable>
        public override bool Equals(object other)
        {
            if (this == other) return true;
            if (other == null) return false;
            try
            {
                ByteArrayWrapper that = (ByteArrayWrapper)other;
                if (Count != that.Count) return false;
                for (int i = 0; i < Count; ++i)
                {
                    if (bytes[i] != that.bytes[i]) return false;
                }
                return true;
            }
            catch (InvalidCastException e)
            {
            }
            return false;
        }

        /// <summary>
        /// Return the hashcode.
        /// </summary>
        /// <returns>the hashcode.</returns>
        /// <stable>ICU 3.2</stable>
        public override int GetHashCode()
        {
            int result = bytes.Length;
            for (int i = 0; i < Count; ++i)
            {
                result = 37 * result + bytes[i];
            }
            return result;
        }

        /// <summary>
        /// Compare this object to another <see cref="ByteArrayWrapper"/>, which must not be null.
        /// </summary>
        /// <param name="other">the object to compare to.</param>
        /// <returns>a value &lt;0, 0, or &gt;0 as this compares less than, equal to, or
        /// greater than other.</returns>
        /// <stable>ICU 4.4</stable>
        public virtual int CompareTo(ByteArrayWrapper other)
        {
            if (this == other) return 0;
            int minSize = Count < other.Count ? Count : other.Count;
            for (int i = 0; i < minSize; ++i)
            {
                if (bytes[i] != other.bytes[i])
                {
                    return (bytes[i] & 0xFF) - (other.bytes[i] & 0xFF);
                }
            }
            return Count - other.Count;
        }

        // private methods -----------------------------------------------------

        /// <summary>
        /// Copies the contents of <paramref name="src"/> byte array from offset <paramref name="srcoff"/> to the
        /// target of <paramref name="tgt"/> byte array at the offset <paramref name="tgtoff"/>.
        /// </summary>
        /// <param name="src">Source byte array to copy from.</param>
        /// <param name="srcoff">Start offset of src to copy from.</param>
        /// <param name="tgt">Target byte array to copy to.</param>
        /// <param name="tgtoff">Start offset of tgt to copy to.</param>
        /// <param name="length">Size of contents to copy.</param>
        private static void CopyBytes(byte[] src, int srcoff, byte[] tgt,
                                           int tgtoff, int length)
        {
            if (length < 64)
            {
                for (int i = srcoff, n = tgtoff; --length >= 0; ++i, ++n)
                {
                    tgt[n] = src[i];
                }
            }
            else
            {
                System.Array.Copy(src, srcoff, tgt, tgtoff, length);
            }
        }
    }
}