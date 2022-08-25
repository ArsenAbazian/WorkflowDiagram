## WfRunner class

The WfRunner class allows you to initialize and execute your workflow diagram. Here is the quick example: 

```csharp
WfDocument doc = new WfDocument(); 
WfAbortNode node = new WfAbortNode(true);
doc.AddNode(node);
WfRunner runner = new WfRunner(doc);
bool result = runner.RunOnce(doc);
```
In this example the WfRunner class visits first and only one node WfAbortNode. When this node is visited it terminates execution and returns true as a result value.

### WfRunner Working Overview
Let's now look at the order in which WfRunner bypasses the nodes when we call RunOnce. First, it receives a list of nodes from which to start bypassing. These are nodes that do not depend on other nodes, i.e. do not have input connections. These nodes are immediately added to the list of nodes that WfRunner should visit. These are the current nodes. Then WfRunner constantly performs the following actions:
1. WfRunner iterate through the list of current nodes and checks if curren node can be visited. It depends on whether all the nodes , on which this node depends, have been visited.
2. If not all previous nodes on which the current node depends were visited, then the node is skipped and not processed yet, and we go back to step 1.
3. If the current node can be processed, then WfRunner executes the Node.Visit method.
4. It then gets a list of next nodes that depend on the current node and adds them to the list of current nodes.
5. WfRunner removes visited node from the list of current nodes.
6. If there are no nodes left in the list of current nodes, WfRunner end the bypass. Otherwise, it goes to step 1 and process repeats again.
7. If during the passage of one cycle the list of current nodes has not changed, this means that an error has occurred (infinite cycle) and WfRunner stops the traversal abnormally.
 
