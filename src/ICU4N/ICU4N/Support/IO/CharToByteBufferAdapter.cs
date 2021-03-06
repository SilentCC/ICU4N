﻿using ICU4N.Support.Text;
using System;

namespace ICU4N.Support.IO
{
    /// <summary>
    /// This class wraps a byte buffer to be a char buffer.
    /// </summary>
    /// <remarks>
    /// Implementation notice:
    /// <list type="bullet">
    ///     <item><description>
    ///         After a byte buffer instance is wrapped, it becomes privately owned by
    ///         the adapter. It must NOT be accessed outside the adapter any more.
    ///     </description></item>
    ///     <item><description>
    ///         The byte buffer's position and limit are NOT linked with the adapter.
    ///         The adapter extends Buffer, thus has its own position and limit.
    ///     </description></item>
    /// </list>
    /// </remarks>
    internal sealed class CharToByteBufferAdapter : CharBuffer
    {
        internal static CharBuffer Wrap(ByteBuffer byteBuffer)
        {
            return new CharToByteBufferAdapter(byteBuffer.Slice());
        }

        private readonly ByteBuffer byteBuffer;

        internal CharToByteBufferAdapter(ByteBuffer byteBuffer)
                : base((byteBuffer.Capacity >> 1))
        {
            this.byteBuffer = byteBuffer;
            this.byteBuffer.Clear();
        }

        //public int GetByteCapacity()
        //{
        //    if (byteBuffer is IDirectBuffer)
        //    {
        //        return ((DirectBuffer)byteBuffer).getByteCapacity();
        //    }
        //    Debug.Assert(false, byteBuffer);
        //    return -1;
        //}

        //public PlatformAddress getEffectiveAddress()
        //{
        //    if (byteBuffer instanceof DirectBuffer) {
        //        return ((DirectBuffer)byteBuffer).getEffectiveAddress();
        //    }
        //    assert false : byteBuffer;
        //    return null;
        //}

        //public PlatformAddress getBaseAddress()
        //{
        //    if (byteBuffer instanceof DirectBuffer) {
        //        return ((DirectBuffer)byteBuffer).getBaseAddress();
        //    }
        //    assert false : byteBuffer;
        //    return null;
        //}

        //public boolean isAddressValid()
        //{
        //    if (byteBuffer instanceof DirectBuffer) {
        //        return ((DirectBuffer)byteBuffer).isAddressValid();
        //    }
        //    assert false : byteBuffer;
        //    return false;
        //}

        //public void addressValidityCheck()
        //{
        //    if (byteBuffer instanceof DirectBuffer) {
        //        ((DirectBuffer)byteBuffer).addressValidityCheck();
        //    } else {
        //        assert false : byteBuffer;
        //    }
        //}

        //public void free()
        //{
        //    if (byteBuffer instanceof DirectBuffer) {
        //        ((DirectBuffer)byteBuffer).free();
        //    } else {
        //        assert false : byteBuffer;
        //    }
        //}

        public override CharBuffer AsReadOnlyBuffer()
        {
            CharToByteBufferAdapter buf = new CharToByteBufferAdapter(byteBuffer
                    .AsReadOnlyBuffer());
            buf.limit = limit;
            buf.position = position;
            buf.mark = mark;
            return buf;
        }

        public override CharBuffer Compact()
        {
            if (byteBuffer.IsReadOnly)
            {
                throw new ReadOnlyBufferException();
            }
            byteBuffer.Limit = limit << 1;
            byteBuffer.Position = position << 1;
            byteBuffer.Compact();
            byteBuffer.Clear();
            position = limit - position;
            limit = capacity;
            mark = UNSET_MARK;
            return this;
        }

        public override CharBuffer Duplicate()
        {
            CharToByteBufferAdapter buf = new CharToByteBufferAdapter(byteBuffer
                    .Duplicate());
            buf.limit = limit;
            buf.position = position;
            buf.mark = mark;
            return buf;
        }

        public override char Get()
        {
            if (position == limit)
            {
                throw new BufferUnderflowException();
            }
            return byteBuffer.GetChar(position++ << 1);
        }

        public override char Get(int index)
        {
            if (index < 0 || index >= limit)
            {
                throw new IndexOutOfRangeException();
            }
            return byteBuffer.GetChar(index << 1);
        }

        public override bool IsDirect
        {
            get { return byteBuffer.IsDirect; }
        }

        public override bool IsReadOnly
        {
            get { return byteBuffer.IsReadOnly; }
        }

        public override ByteOrder Order
        {
            get { return byteBuffer.Order; }
        }

        protected override char[] ProtectedArray
        {
            get { throw new NotSupportedException(); }
        }

        protected override int ProtectedArrayOffset
        {
            get { throw new NotSupportedException(); }
        }

        protected override bool ProtectedHasArray
        {
            get { return false; }
        }

        public override CharBuffer Put(char c)
        {
            if (position == limit)
            {
                throw new BufferOverflowException();
            }
            byteBuffer.PutChar(position++ << 1, c);
            return this;
        }

        public override CharBuffer Put(int index, char c)
        {
            if (index < 0 || index >= limit)
            {
                throw new IndexOutOfRangeException();
            }
            byteBuffer.PutChar(index << 1, c);
            return this;
        }

        public override CharBuffer Slice()
        {
            byteBuffer.Limit = limit << 1;
            byteBuffer.Position = position << 1;
            CharBuffer result = new CharToByteBufferAdapter(byteBuffer.Slice());
            byteBuffer.Clear();
            return result;
        }

        public override ICharSequence SubSequence(int start, int end)
        {
            if (start < 0 || end < start || end > Remaining)
            {
                throw new IndexOutOfRangeException();
            }

            CharBuffer result = Duplicate();
            result.Limit = position + end;
            result.Position = position + start;
            return result;
        }
    }
}
