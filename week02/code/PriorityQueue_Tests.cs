using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue First with priority 1 and Second with priority 5.
    // Expected Result: Both items should be added to the back of the queue
    // and remain in the same order in which they were added.
    // Defect(s) Found: No defect was found. Enqueue correctly added each
    // new item to the back of the queue.
    public void TestPriorityQueue_EnqueueAddsToBack()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 5);

        Assert.AreEqual(
            "[First (Pri:1), Second (Pri:5)]",
            priorityQueue.ToString()
        );
    }

    [TestMethod]
    // Scenario: Enqueue Low with priority 1, Medium with priority 3, and
    // High with priority 5. The highest-priority item is at the end.
    // Expected Result: High should be dequeued first.
    // Defect(s) Found: Medium was returned instead of High because the loop
    // stopped before examining the final item in the queue.
    public void TestPriorityQueue_HighestPriorityItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue First and Second with the same highest priority of 5,
    // followed by Low with priority 1.
    // Expected Result: First should be returned because items with equal
    // priority must follow FIFO order.
    // Defect(s) Found: Second was returned instead of First because the
    // comparison used >=. This caused the later equal-priority item to replace
    // the earlier item as the selected item.
    public void TestPriorityQueue_EqualPriorityUsesFifo()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Low", 1);

        Assert.AreEqual("First", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue High with priority 5, Low with priority 1, and Tail
    // with priority 0. Dequeue multiple times.
    // Expected Result: High should be removed during the first call. The
    // second call should return Low, followed by Tail.
    // Defect(s) Found: High was returned repeatedly because Dequeue returned
    // its value without removing the item from the queue.
    public void TestPriorityQueue_DequeueRemovesItem()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Tail", 0);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
        Assert.AreEqual("Tail", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue an item from an empty priority queue.
    // Expected Result: An InvalidOperationException should be thrown with
    // the exact message "The queue is empty."
    // Defect(s) Found: No defect was found. The correct exception type and
    // exact error message were produced.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                $"Unexpected exception of type {e.GetType()} caught: {e.Message}"
            );
        }
    }
}