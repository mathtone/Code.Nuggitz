
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Nuggitz {

	
	public class TreeSearch {

		public void BFS_Test() {

		}

		[Fact]
		public void DFS_Test() {

		}
	}

	//Time complexity = O(N+M)
	//Time complexity = O(N+M)
	public class Node<T> {
		public T Value { get; set; }
		public List<Node<T>> Children { get; } = new List<Node<T>>();

		public Node() : this(default(T)) { }
		public Node(T value) {
			this.Value = value;
		}

		public IEnumerable<Node<T>> TraverseDepthFirst() {
			yield return this;
			foreach(var c1 in this.Children) {
				foreach(var c2 in c1.TraverseDepthFirst()) {
					yield return c2;
				}
			}
		}

		public IEnumerable<Node<T>> TraverseBreadthFirst() {
			yield return this;
			var children = this.Children as IEnumerable<Node<T>>;
			while(children.Any()) {
				foreach(var c in children) {
					yield return c;
				}
				children = children.SelectMany(n => n.Children);
			}
		}
	}
}