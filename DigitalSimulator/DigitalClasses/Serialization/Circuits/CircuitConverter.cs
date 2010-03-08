using System;
using System.Collections.Generic;
using DigitalClasses.Logic;
using DigitalClasses.Graphic;
using System.Drawing;
using DigitalClasses.Exceptions;
using DigitalClasses.Controls;

namespace DigitalClasses.Serialization
{
    /// <summary>
    /// This class converts circuitry classes to their corresponding serializable data classes and vice versa
    /// </summary>
    public class CircuitConverter
    {
        #region Fields

        private delegate object ConvertMethod(object Input);

        private static CircuitConverter m_Instance = new CircuitConverter();
        private List<Type> m_ElementTypes;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the singleton Instance for this class
        /// </summary>
        public static CircuitConverter Instance
        {
            get
            {
                return m_Instance;
            }
        }

        #endregion

        #region Construction

        private CircuitConverter()
        {
            #region build type dictionary
            m_ElementTypes = new List<Type>();
            m_ElementTypes.Add(typeof(AndGate));
            m_ElementTypes.Add(typeof(NandGate));
            m_ElementTypes.Add(typeof(OrGate));
            m_ElementTypes.Add(typeof(NorGate));
            m_ElementTypes.Add(typeof(BufferGate));
            m_ElementTypes.Add(typeof(NotGate));
            m_ElementTypes.Add(typeof(XorGate));
            m_ElementTypes.Add(typeof(XnorGate));
            m_ElementTypes.Add(typeof(Clock));
            m_ElementTypes.Add(typeof(ConstantInput));
            m_ElementTypes.Add(typeof(SignalInput));
            m_ElementTypes.Add(typeof(SignalOutput));
            #endregion
        }

        #endregion

        #region Public Implementation

        /// <summary>
        /// Converts a Circuit instance into seraliazable CircuitData object
        /// </summary>
        /// <param name="circuit">the circuit to convert</param>
        /// <returns>converted circuitdata</returns>
        public CircuitData ConvertFromCircuit(Circuit circuit)
        {
            CircuitData circuitData = new CircuitData(circuit.Name);
            CreateElementData(circuitData, circuit);
            return circuitData;
        }

        /// <summary>
        /// Converts (restores) deserialized CircuitData object into a Circuit instance.
        /// Graphical objects for GUI are not created. 
        /// </summary>
        /// <param name="circuitData">deserialized CircuitData object</param>
        /// <returns>restored Circuit instance</returns>
        public Circuit ConvertToCircuit(CircuitData circuitData)
        {
            return ConvertToCircuit(circuitData, null, false);
        }

        /// <summary>
        /// Converts (restores) deserialized CircuitData object into a Circuit instance.
        /// Creation of graphical objects for GUI is determined by parameter.
        /// </summary>
        /// <param name="circuitData">deserialized CircuitData object</param>
        /// <param name="createGraphics">states whether to create graphical objects</param>
        /// <returns>restored Circuit instance</returns>
        public Circuit ConvertToCircuit(CircuitData circuitData, bool createGraphics)
        {
            return ConvertToCircuit(circuitData, null, createGraphics);
        }

        /// <summary>
        /// Converts (restores) deserialized CircuitData object into a Circuit instance.
        /// Creation of graphical objects for GUI is determined by parameter.
        /// </summary>
        /// <param name="circuitData">deserialized CircuitData object</param>
        /// <param name="signals">the corresponding Signals for the circuit</param>
        /// <param name="createGraphics">states whether to create graphical objects</param>
        /// <returns>restored Circuit instance</returns>
        public Circuit ConvertToCircuit(CircuitData circuitData, SignalList signals, bool createGraphics)
        {
            Circuit circuit = new Circuit();
            CreateElements(circuit, circuitData, signals, createGraphics);
            return circuit;
        }

        #endregion

        #region Private Implementation

        private void CreateElementData(CircuitData circuitData, Circuit circuit)
        {
            List<BaseElementData> elements = new List<BaseElementData>();
            List<ConnectionData> connections = new List<ConnectionData>();
            foreach (BaseElement be in circuit)
            {
                if (be is Connection)
                {
                    ConnectionData connData = ConvertConnection(be);
                    connections.Add(connData);
                }
                else if (be is InputOutputElement)
                {
                    BaseElementData elemData = ConvertIOElement(be);
                    if (elemData != null)
                    {
                        elements.Add(elemData);
                    }
                }
            }
            circuitData.Elements = elements.ToArray();
            circuitData.Connections = connections.ToArray();
        }

        private void CreateElements(Circuit circuit, CircuitData circuitData, SignalList signals, bool createGraphics)
        {
            Dictionary<BaseElement, BaseElementData> elemDict = new Dictionary<BaseElement, BaseElementData>();
            foreach (BaseElementData elemData in circuitData.Elements)
            {
                InputOutputElement element = null;
                element = ConvertElementData(elemData, createGraphics, elemDict, signals);
                if (element != null)
                {
                    circuit.AddElement(element);
                }
            }
            foreach (ConnectionData connectionData in circuitData.Connections)
            {
                Connection connection = new Connection();
                connection.Name = connectionData.Name;
                ConnectTerminals(connection, circuit, elemDict);
                if (connection.Terminals.Count > 0)
                {
                    if (createGraphics)
                    {
                        GraphicConnection graphicConnection =
                            GraphicObjectFactory.CreateInstance(typeof(Connection), connection) as GraphicConnection;
                        CreateConnectionLines(circuit, graphicConnection, connectionData);
                    }
                    circuit.AddElement(connection);
                }
            }
        }

        #endregion

        #region ConvertMethods

        private TerminalData ConvertTerminal(BaseElement element)
        {
            Terminal terminal = (Terminal)element;
            TerminalData data = new TerminalData(terminal.Name, terminal.Direction);
            if (terminal.IsConnected)
            {
                data.Connection = terminal.Connection.Name;
            }
            return data;
        }

        private TerminalData[] ConvertTerminalArray(Terminal[] terminalArray)
        {
            List<TerminalData> terminals = new List<TerminalData>(terminalArray.Length);
            foreach (Terminal term in terminalArray)
            {
                terminals.Add(ConvertTerminal(term));
            }
            return terminals.ToArray();
        }

        private ConnectionData ConvertConnection(BaseElement element)
        {
            Connection connection = (Connection)element;
            ConnectionData connData = new ConnectionData(connection.Name);
            connData.Items = ConvertConnectionItems(connection);
            return connData;
        }

        private ConnectionItemData[] ConvertConnectionItems(Connection connection)
        {
            GraphicConnection graphicConnection = connection.LinkedObject as GraphicConnection;
            List<ConnectionItemData> itemdatalist = new List<ConnectionItemData>(graphicConnection.Children.Count);

            foreach (IConnectionItem item in graphicConnection.Children)
            {
                if (item is ConnectionLine)
                {
                    ConnectionLine line = item as ConnectionLine;
                    LineItemData linedata = new LineItemData(line.Nodes[0].Name, line.Nodes[1].Name);
                    itemdatalist.Add(linedata);
                }
                if (item is ConnectionNode)
                {
                    ConnectionNode node = item as ConnectionNode;
                    //if (node.Parent is GraphicTerminal) //impossible
                    //{
                    //    NodeItemData nodedata = new NodeItemData(node.Name);
                    //    nodedata.IsTerminalNode = true; //not needed
                    //    itemdatalist.Add(nodedata);
                    //} else
                    if (node.Parent is GraphicConnection)
                    {
                        NodeItemData nodedata = new NodeItemData(node.Name);
                        //nodedata.IsTerminalNode = false; //not needed
                        nodedata.X = node.Location.X;
                        nodedata.Y = node.Location.Y;
                        itemdatalist.Add(nodedata);
                    }
                }
            }

            return itemdatalist.ToArray();
        }

        private BaseElementData ConvertIOElement(BaseElement element)
        {
            BaseElementData elemData = null;
            if (element is Macro)
            {
                Macro macro = (Macro)element;
                MacroElementData macroData = new MacroElementData(macro.Name, macro.TypeName, macro.FileReference);
                macroData.Inputs = ConvertTerminalArray(macro.Inputs);
                macroData.Outputs = ConvertTerminalArray(macro.Outputs);
                macroData.Index = macro.UpdateIndex;
                macroData.UnitDelay = macro.UnitDelay;
                macroData.NegativeEdgeDelay = macro.NegEdgeDelay;
                macroData.PositiveEdgeDelay = macro.PosEdgeDelay;
                elemData = macroData;
            }
            if (element is Clock)
            {
                Clock clock = (Clock)element;
                ClockData clockData = new ClockData(clock.Name, clock.LowDuration, clock.HighDuration);
                clockData.Outputs = ConvertTerminalArray(clock.Outputs);
                elemData = clockData;
            }
            if (element is SignalInput)
            {
                SignalInput sigInput = (SignalInput)element;
                InputElementData inData = new InputElementData(sigInput.Name, sigInput.SignalName);
                inData.Outputs = ConvertTerminalArray(sigInput.Outputs);
                elemData = inData;
            }
            else if (element is ConstantInput)
            {
                ConstantInput conInput = (ConstantInput)element;
                ConstantInputData inData = new ConstantInputData(conInput.Name, conInput.State.ToString());
                inData.Outputs = ConvertTerminalArray(conInput.Outputs);
                elemData = inData;
            }
            if (element is SignalOutput)
            {
                SignalOutput sigOutput = (SignalOutput)element;
                OutputElementData outData = new OutputElementData(sigOutput.Name, sigOutput.SignalName);
                outData.Inputs = ConvertTerminalArray(sigOutput.Inputs);
                elemData = outData;
            }
            if (elemData == null && element is InputOutputElement)
            {
                InputOutputElement io = (InputOutputElement)element;
                IOElementData ioData = new IOElementData(io.Name, element.GetType().Name);
                ioData.Inputs = ConvertTerminalArray(io.Inputs);
                ioData.Outputs = ConvertTerminalArray(io.Outputs);
                ioData.Index = io.UpdateIndex;
                ioData.UnitDelay = io.UnitDelay;
                ioData.NegativeEdgeDelay = io.NegEdgeDelay;
                ioData.PositiveEdgeDelay = io.PosEdgeDelay;
                elemData = ioData;
            }
            if (elemData == null)
            {
                return null;
            }
            if (element.LinkedObject != null)
            {
                GraphicInputOutputElement grIO = element.LinkedObject as GraphicInputOutputElement;
                elemData.X = grIO.Location.X;
                elemData.Y = grIO.Location.Y;
            }
            return elemData;
        }

        private Terminal ConvertTerminalData(TerminalData terminalData)
        {
            Terminal terminal = new Terminal(terminalData.Direction);
            terminal.Name = terminalData.Name;
            return terminal;
        }

        //private Connection ConvertConnectionData(ConnectionData connectionData) //directly done in CreateElements()
        //{
        //    Connection connection = new Connection();
        //    connection.Name = connectionData.Name;
        //    return connection;
        //}

        private InputOutputElement ConvertElementData(BaseElementData elemData, bool createGraphics,
            Dictionary<BaseElement, BaseElementData> elemDict, SignalList signals)
        {
            InputOutputElement io = null;
            //determine type
            string desiredType = String.Empty;
            if (elemData is ClockData)
            {
                desiredType = typeof(Clock).Name;
            }
            if (elemData is ConstantInputData)
            {
                desiredType = typeof(ConstantInput).Name;
            }
            if (elemData is InputElementData)
            {
                desiredType = typeof(SignalInput).Name;
            }
            if (elemData is OutputElementData)
            {
                desiredType = typeof(SignalOutput).Name;
            }
            if (elemData is IOElementData)
            {
                desiredType = (elemData as IOElementData).Type;
            }
            if (elemData is MacroElementData)
            {
                desiredType = typeof(Macro).Name;
                MacroCache cache = MacroCache.Instance;
                MacroElementData macroElementData = elemData as MacroElementData;
                io = cache.GetMacro(macroElementData.Type);
                if (io == null)
                {
                    string loaded = cache.LoadMacro(macroElementData.Reference);
                    if (String.IsNullOrEmpty(loaded))
                    {
                        throw new MacroReferenceNotFoundException(String.Format("Macro not found: \"{0}\".", macroElementData.Type));
                    }
                    else if (loaded.CompareTo(macroElementData.Type) != 0)
                    {
                        throw new MacroReferenceTypeMismatchException(String.Format("Desired macro type \"{0}\" mismatches type \"{1}\" contained in file \"{2}\".", macroElementData.Type, loaded, macroElementData.Reference));
                    }
                    io = cache.GetMacro(macroElementData.Type);
                }
                if (createGraphics)
                {
                    GraphicBaseElement graphic = GraphicObjectFactory.CreateInstance(typeof(Macro), io);
                    graphic.Location = new PointF(elemData.X, elemData.Y);
                }
            }
            if (String.IsNullOrEmpty(desiredType))
            {
                throw new NotImplementedException(String.Format("Restoring of Type {0} not implemented", elemData.GetType().Name));
                //return null;
            }
            //create instance of io element type type (not macros)
            foreach (Type type in m_ElementTypes)
            {
                if (type.Name.Equals(desiredType))
                {
                    io = (InputOutputElement)Activator.CreateInstance(type);

                    if (elemData is InputElementData)
                    {
                        InputElementData inelemData = elemData as InputElementData;
                        if (signals != null)
                            foreach (Signal signal in signals)
                            {
                                if (signal.Name.Equals(inelemData.SignalName))
                                {
                                    (io as SignalInput).Signal = signal;
                                }
                            }
                    }

                    if (createGraphics)
                    {
                        GraphicBaseElement graphic = GraphicObjectFactory.CreateInstance(type, io);
                        graphic.Location = new PointF(elemData.X, elemData.Y);
                    }
                    break;
                }
            }
            if (io == null)
            {
                return null;
            }
            //restore terminals
            if (elemData is ClockData)
            {
                ClockData clockData = elemData as ClockData;
                io.OutputCount = clockData.Outputs.Length;
                RestoreTerminals(clockData.Outputs, io.Outputs);
                Clock clock = io as Clock;
                clock.HighDuration = clockData.HighDuration;
                clock.LowDuration = clockData.LowDuration;
            }
            if (elemData is ConstantInputData)
            {
                ConstantInputData inelemData = elemData as ConstantInputData;
                StateTypeConverter stateconv = new StateTypeConverter();
                (io as ConstantInput).State = (State)stateconv.ConvertFromString(inelemData.State.Trim());
                io.OutputCount = inelemData.Outputs.Length;
                RestoreTerminals(inelemData.Outputs, io.Outputs);
            }
            if (elemData is InputElementData)
            {
                InputElementData inelemData = elemData as InputElementData;
                io.OutputCount = inelemData.Outputs.Length;
                RestoreTerminals(inelemData.Outputs, io.Outputs);
                if (signals != null)
                    foreach (Signal signal in signals)
                    {
                        if (signal.Name.Equals(inelemData.SignalName))
                        {
                            (io as SignalInput).Signal = signal;
                        }
                    }
            }
            if (elemData is OutputElementData)
            {
                OutputElementData outelemData = elemData as OutputElementData;
                (io as SignalOutput).SignalName = outelemData.SignalName;
                io.InputCount = outelemData.Inputs.Length;
                RestoreTerminals(outelemData.Inputs, io.Inputs);
            }
            if (elemData is IOElementData || elemData is MacroElementData)
            {
                IOElementData ioelemData = elemData as IOElementData;
                io.InputCount = ioelemData.Inputs.Length;
                io.OutputCount = ioelemData.Outputs.Length;
                RestoreTerminals(ioelemData.Inputs, io.Inputs);
                RestoreTerminals(ioelemData.Outputs, io.Outputs);
                io.UpdateIndex = ioelemData.Index;
                io.UnitDelay = ioelemData.UnitDelay;
                io.NegEdgeDelay = ioelemData.NegativeEdgeDelay;
                io.PosEdgeDelay = ioelemData.PositiveEdgeDelay;
            }
            io.Name = elemData.Name;
            elemDict.Add(io, elemData);
            return io;
        }

        private static void RestoreTerminals(TerminalData[] termData, Terminal[] terminals)
        {
            for (int i = 0; i < terminals.Length; i++)
            {
                terminals[i].Name = termData[i].Name;
            }
        }

        private void ConnectTerminals(Connection connection, Circuit circuit, Dictionary<BaseElement, BaseElementData> elemDict)
        {
            foreach (BaseElement be in circuit)
            {
                if (be is InputOutputElement == false)
                {
                    continue;
                }
                BaseElementData elemData = elemDict[be];
                if (elemData == null)
                {
                    continue;
                }
                InputOutputElement io = be as InputOutputElement;
                if (elemData is IOElementData)
                {
                    IOElementData ioelemData = elemData as IOElementData;
                    ProcessTerminalList(ioelemData.Inputs, io.Inputs, connection);
                    ProcessTerminalList(ioelemData.Outputs, io.Outputs, connection);
                }
                if (elemData is InputElementData)
                {
                    InputElementData ioelemData = elemData as InputElementData;
                    ProcessTerminalList(ioelemData.Outputs, io.Outputs, connection);
                }
                else if (elemData is ConstantInputData)
                {
                    ConstantInputData ioelemData = elemData as ConstantInputData;
                    ProcessTerminalList(ioelemData.Outputs, io.Outputs, connection);
                }
                if (elemData is ClockData)
                {
                    ClockData ioelemData = elemData as ClockData;
                    ProcessTerminalList(ioelemData.Outputs, io.Outputs, connection);
                }
                if (elemData is OutputElementData)
                {
                    OutputElementData ioelemData = elemData as OutputElementData;
                    ProcessTerminalList(ioelemData.Inputs, io.Inputs, connection);
                }
            }
        }

        private void ProcessTerminalList(TerminalData[] termData, Terminal[] terminals, Connection connection)
        {
            for (int i = 0; i < terminals.Length; i++)
            {
                string connName = termData[i].Connection;
                if (String.IsNullOrEmpty(connName) == false && connName.Equals(connection.Name))
                {
                    connection.ConnectTerminal(terminals[i]);
                }
            }
        }

        private void CreateConnectionLines(Circuit circuit, GraphicConnection graphicConnection, ConnectionData connectionData)
        {
            if (connectionData.Items == null || connectionData.Items.Length == 0)
            {
                //downward compatibility - no detailed information available: 
                Connection connection = graphicConnection.LinkedObject as Connection;
                GraphicTerminal previous = null;
                //displays straight lines from one terminal to another
                foreach (Terminal terminal in connection.Terminals)
                {
                    GraphicTerminal graphicTerminal = terminal.LinkedObject as GraphicTerminal;
                    if (previous != null)
                    {
                        graphicConnection.AddChild(new ConnectionLine(previous.ConnectionNode, graphicTerminal.ConnectionNode));
                    }
                    previous = graphicTerminal;
                }
                return;
            }
            //create nodes
            foreach (ConnectionItemData itemData in connectionData.Items)
            {
                if (itemData is NodeItemData)
                {
                    NodeItemData nodeData = itemData as NodeItemData;
                    ConnectionNode node = new ConnectionNode(nodeData.X, nodeData.Y);
                    graphicConnection.AddChild(node);
                }
            }
            //create lines
            foreach (ConnectionItemData itemData in connectionData.Items)
            {
                if (itemData is LineItemData)
                {
                    LineItemData lineData = itemData as LineItemData;

                    ConnectionNode node1 = SearchNode(lineData.Node1, circuit, graphicConnection);
                    ConnectionNode node2 = SearchNode(lineData.Node2, circuit, graphicConnection);

                    if (node1 != null && node2 != null)
                    {
                        ConnectionLine line = new ConnectionLine(node1, node2);
                        graphicConnection.AddChild(line);
                    }
                }
            }
        }

        private ConnectionNode SearchNode(string nodeName, Circuit circuit, GraphicConnection graphicConnection)
        {
            //node is already part of the connection
            if (graphicConnection.Children != null)
            {
                foreach (GraphicBaseElement element in graphicConnection.Children)
                {
                    if (element is ConnectionNode && element.Name.Equals(nodeName))
                    {
                        return element as ConnectionNode;
                    }
                }
            }
            //so the node must be part of a terminal, the name gives the clue
            string[] str = nodeName.Split(new char[] { '/' }, 2);
            if (str.Length == 2)
            {
                string elementName = str[0];
                string terminalName = str[1];

                foreach (BaseElement element in circuit)
                {
                    //for security reasons check whether the found terminal is connected to the connection too
                    //although the name should be all that's needed
                    Connection connection = graphicConnection.LinkedObject as Connection;
                    if (element is InputOutputElement && element.Name.Equals(elementName))
                    {
                        InputOutputElement io = element as InputOutputElement;
                        foreach (Terminal terminal in io.Inputs)
                        {
                            if (connection.Terminals.Contains(terminal) && terminal.Name.Equals(terminalName))
                            {
                                return (terminal.LinkedObject as GraphicTerminal).ConnectionNode;
                            }
                        }
                        foreach (Terminal terminal in io.Outputs)
                        {
                            if (connection.Terminals.Contains(terminal) && terminal.Name.Equals(terminalName))
                            {
                                return (terminal.LinkedObject as GraphicTerminal).ConnectionNode;
                            }
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
