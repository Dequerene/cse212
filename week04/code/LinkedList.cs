using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);

        // If the list is empty, point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, only the head will be affected.
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new(value);

        // If the list is empty, both head and tail point to the new node.
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            // Connect the current tail to the new node.
            newNode.Prev = _tail;
            _tail.Next = newNode;

            // Update the tail.
            _tail = newNode;
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // This condition covers an empty list and a one-node list.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item, update the head.
        else if (_head is not null)
        {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // This condition covers an empty list and a one-node list.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item, update the tail.
        else if (_tail is not null)
        {
            _tail = _tail.Prev;
            _tail!.Next = null;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value'
    /// in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the matching value is at the tail, use InsertTail.
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);

                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }

                // Stop after the first occurrence.
                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    // Connect the nodes before and after the removed node.
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }

                // Remove only the first matching node.
                return;
            }

            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace
    /// the value with 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }

            // Continue to replace every matching value.
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the linked list.
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        Node? curr = _head;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the linked list.
    /// </summary>
    public IEnumerable Reverse()
    {
        Node? curr = _tail;

        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public bool HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public bool HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" +
               string.Join(", ", array.Cast<int>()) +
               "}";
    }
}