using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Test cases written from the requirements; defects fixed in PriorityQueue.cs.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue A(1), B(5), C(3). Dequeue three times.
    // Expected Result: B, C, A (highest priority first; each item is removed once returned)
    // Defect(s) Found: Dequeue never removed the item from the list, so the same value was returned
    // again on the next call. Fixed by calling RemoveAt on the selected index.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A(5), B(5), C(1), D(5). Dequeue four times.
    // Expected Result: A, B, D, C (ties on the highest priority are served FIFO)
    // Defect(s) Found: The comparison used >=, so a later item with equal priority replaced the
    // earlier one (D was returned before A). Fixed by using strict >.
    public void TestPriorityQueue_TiesAreFifo()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 1);
        priorityQueue.Enqueue("D", 5);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A(1), B(2), C(9) so the highest priority item is the last one. Dequeue once.
    // Expected Result: C
    // Defect(s) Found: The loop condition was index < Count - 1, so the last item was never
    // examined. Fixed by looping to index < Count.
    public void TestPriorityQueue_HighestAtBack()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A(5), B(1), C(1). Dequeue three times.
    // Expected Result: A, B, C (highest first, then FIFO among the equal lower priorities)
    // Defect(s) Found: Failed before the fixes because Dequeue did not remove the item, so A was
    // returned again instead of B. Passes after the RemoveAt fix.
    public void TestPriorityQueue_FifoAmongLowerTies()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 1);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. The empty check was already correct.
    public void TestPriorityQueue_Empty()
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
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }
}
