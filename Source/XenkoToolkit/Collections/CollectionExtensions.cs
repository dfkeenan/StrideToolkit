using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Collections
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            foreach (var item in values)
            {
                collection.Add(item);
            }
        }

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> collection)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }


            foreach (var item in collection)
            {
                queue.Enqueue(item);
            }
        }

        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> collection)
        {
            if (stack == null)
            {
                throw new ArgumentNullException(nameof(stack));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }


            foreach (var item in collection)
            {
                stack.Push(item);
            }
        }

        public static T Peek<T>(this IList<T> stack) => stack[stack.Count - 1];

        public static void Push<T>(this IList<T> stack, T item) => stack.Add(item);

        public static T Pop<T>(this IList<T> stack)
        {
            var item = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return item;
        }

        public static T PopFront<T>(this IList<T> stack)
        {
            var item = stack[0];
            stack.RemoveAt(0);
            return item;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second)
        {
            return Enumerable.Concat(first, second);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var item in source)
            {
                action(item);
            }
        }

        public static void MergeInto<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> target)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var item in source)
                target[item.Key] = item.Value;
        }

        public static TValue GetOrDefault<TKey,TValue>(this IDictionary<TKey, TValue> dicionary, TKey key, TValue defaultValue = default(TValue))
        {
            if (dicionary == null)
                throw new ArgumentNullException(nameof(dicionary));

            TValue result = default(TValue);

            if (dicionary.TryGetValue(key, out result))
            {
                return result;
            }

            return defaultValue;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dicionary, TKey key, Func<TKey, TValue> getValue)
        {
            if (dicionary == null)
                throw new ArgumentNullException(nameof(dicionary));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));


            TValue result = default(TValue);

            if (!dicionary.TryGetValue(key, out result))
            {
                dicionary[key] = result = getValue(key);
            }

            return result;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dicionary, TKey key, Func<TKey, TValue> getValue, Func<TValue, bool> shouldAdd)
        {
            if (dicionary == null)
                throw new ArgumentNullException(nameof(dicionary));

            if (getValue == null)
                throw new ArgumentNullException(nameof(getValue));


            TValue result = default(TValue);

            if (!dicionary.TryGetValue(key, out result))
            {
                result = getValue(key);

                if (shouldAdd(result))
                    dicionary[key] = result;
            }

            return result;
        }

        public static int Increment<TKey>(this IDictionary<TKey, int> dicionary, TKey key)
        {
            if (dicionary == null)
                throw new ArgumentNullException(nameof(dicionary));


            int result = default(int);
            dicionary[key] = dicionary.TryGetValue(key, out result) ? result += 1 : result = 1;
            return result;
        }
    }
}
