using System;
using System.Collections.Generic;
using DigitalClasses.Logic;

namespace DigitalClasses.Events
{
    /// <summary>
    /// Event for simple notifying
    /// </summary>
    /// <param name="sender">The source of the event</param>
    public delegate void NotifyEvent(object sender);

    /// <summary>
    /// Event on change of the location of an element
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Event arguments containing further information</param>
    public delegate void LocationChangeEvent(object sender, LocationChangedEventArgs e);

    /// <summary>
    /// Event on adding some part to a symbol
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing part</param>
    public delegate void NewSymbolPart(object sender, NewSymbolPartEventArgs e);

    /// <summary>
    /// Event on adding an element
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing element</param>
    public delegate void NewElement(object sender, NewElementEventArgs e);

    /// <summary>
    /// Event on selecting an element in the circuit editor
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing element</param>
    public delegate void ElementSelected(object sender, ElementSelectedEventArgs e);

    /// <summary>
    /// Event on selecting a part in the symbol editor
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing part</param>
    public delegate void PartSelected(object sender, PartSelectedEventArgs e);

    /// <summary>
    /// Event when the drawing needs update
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="completeDrawing">true, if the complete drawing needs update, not only the particular element</param>
    public delegate void UpdateDrawingEvent(object sender, bool completeDrawing);

    /// <summary>
    /// Event on change of UpdateIndex of a InputOutputElement
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing element</param>
    public delegate void UpdateIndexChanged(object sender, UpdateIndexChangedEventArgs e);

    /// <summary>
    /// Event on change of terminal count of a InputOutputElement
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="e">Arguments containing element</param>
    public delegate void TerminalCountChanged(object sender, TerminalCountChangedEventArgs e);

    /// <summary>
    /// Enumeration used to depict the type of change of a signal
    /// </summary>
    public enum SignalChangeType
    {
        SignalGraph,
        SignalName
    }

    /// <summary>
    /// Event on change of a Signal
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="change">States what changed in the signal</param>
    public delegate void SignalChanged(object sender, SignalChangeType change);

    /// <summary>
    /// Event on change of a Simulation
    /// </summary>
    /// <param name="sender">Source of the event</param>
    /// <param name="newState">The new simulation state that occured</param>
    public delegate void SimulationStateChanged(object sender, SimulationState newState);
}
