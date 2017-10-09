using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Collections
{
    public static class ListExtensions
    {
        public static T Peek<T>(this IList<T> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            return stack[stack.Count - 1];
        }

        public static void Push<T>(this IList<T> stack, T item)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            stack.Add(item);
        }

        public static T Pop<T>(this IList<T> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            var item = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return item;
        }

        public static T PopFront<T>(this IList<T> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            var item = stack[0];
            stack.RemoveAt(0);
            return item;
        }
    }
}
