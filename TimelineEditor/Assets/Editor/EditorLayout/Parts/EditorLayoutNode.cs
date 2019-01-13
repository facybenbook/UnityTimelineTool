﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorLayout {
	
	public abstract class Node {
		private Node m_parent      = null;
		private Node m_child       = null;
		private Node m_prevSibling = null;
		private Node m_nextSibling = null;

		// 親を取得
		public Node parent {
			private set { m_parent = value; }
			get { return m_parent; }
		}

		// 最初の子を取得
		public Node childRoot {
			private set { m_child = value; }
			get { return m_child; }
		}

		// 姉を取得
		public Node prevSibling {
			private set { m_prevSibling = value; }
			get { return m_prevSibling; }
		}

		// 妹を取得
		public Node nextSibling {
			private set { m_nextSibling = value; }
			get { return m_nextSibling; }
		}

		// 子を追加
		public void AddChild(Node addedChild)
		{
			if(childRoot == null) {
				childRoot = addedChild;
			}
			else {
				NodeTraverser traverser = new NodeTraverser(childRoot);
				var lastChild = traverser.FindLastChild();
				lastChild.nextSibling = addedChild;
			}
		}

		// 親にアタッチ
		public void AttachToParent(Node targetParent)
		{
			NodeTraverser traverser = new NodeTraverser(targetParent);
			var lastChild = traverser.FindLastChild();

			parent = targetParent;
			lastChild.nextSibling = this;
			prevSibling = lastChild;
		}

		// 親から切り離す
		public void DetachFromParent()
		{
			if(parent.childRoot == this) {
				parent.childRoot = nextSibling;
			}
			if(prevSibling != null) {
				prevSibling.nextSibling = nextSibling;
			}
			if(nextSibling != null) {
				nextSibling.prevSibling = prevSibling;
			}
			parent = null;
		}

		// 描画
		public abstract void Render();
		
	}

}