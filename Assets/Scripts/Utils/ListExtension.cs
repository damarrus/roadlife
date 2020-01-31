using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils {
    public static class ListExtension {

        /// <summary>
        /// Find Range in SORTED enumerable
        /// </summary>
        public static Range<T> FindRange<T>(this IEnumerable<T> enumerable, T value) where T : IComparable {
            var count = enumerable.Count();
            var result = new Range<T>();
            T previousItem = default;
            var isFirstIteration = true;
            var i = 0;

            foreach (var item in enumerable) {
                result.StartIndex = i;
                if (item.CompareTo(value) > 0) {

                    if (isFirstIteration) {
                        result.End = item;
                    } else {
                        result.Start = previousItem;
                        result.End = item;
                    }
                    break;
                }

                if (i == count - 1) {
                    result.Start = item;
                    break;
                }

                isFirstIteration = false;
                previousItem = item;
                i++;
            }
            return result;
        }
    }

    public class Range<T> where T : IComparable {

        private T start;
        private T end;

        public int StartIndex;
        public bool HasStart { get; private set; }
        public bool HasEnd { get; private set; }

        public T Start {
            get { return start; }
            set {
                HasStart = true;
                start = value;
            }
        }
        public T End {
            get { return end; }
            set {
                HasEnd = true;
                end = value;
            }
        }

        public Range() {

        }

        public Range(int startIndex, T start, T end) {
            StartIndex = startIndex;
            Start = start;
            End = end;
        }
    }
}
