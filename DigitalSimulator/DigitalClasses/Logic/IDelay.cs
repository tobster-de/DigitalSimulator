using System;
using System.Collections.Generic;

namespace DigitalClasses.Logic
{
    /// <summary>
    /// This is the common interface to realize a delay
    /// </summary>
    interface IDelay
    {
        /// <summary>
        /// Indexer to change delay values
        /// </summary>
        /// <param name="index">index of the dalay value</param>
        /// <returns>value of the index</returns>
        int this[int index]
        {
            get;
            set;
        }

        /// <summary>
        /// Method to enqueue a state into a delay
        /// </summary>
        /// <param name="state">state to enqueue</param>
        void Enqueue(State state);

        /// <summary>
        /// Method to dequeue a state from a delay
        /// </summary>
        /// <returns>dequeued state</returns>
        State Dequeue();

        /// <summary>
        /// Returns the next state of the delay without removing it
        /// </summary>
        /// <returns>next state</returns>
        State Peek();
    
    }
}
