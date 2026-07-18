/// <summary>
/// This queue is circular. When people are added via AddPerson, then they are added to the
/// back of the queue according to FIFO rules. When GetNextPerson is called, the next person
/// is removed from the front and returned. If the person still has turns remaining, they are
/// placed back at the end of the queue. A turns value of 0 or less represents infinite turns.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the back of the queue.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person is placed
    /// back into the queue if they have turns remaining. A turns value of
    /// 0 or less represents an infinite number of turns.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Zero or a negative value represents infinite turns.
            // Do not change the original turns value.
            _people.Enqueue(person);
        }
        else
        {
            // Use one finite turn.
            person.Turns--;

            // Return the person to the queue only when turns remain.
            if (person.Turns > 0)
            {
                _people.Enqueue(person);
            }
        }

        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}