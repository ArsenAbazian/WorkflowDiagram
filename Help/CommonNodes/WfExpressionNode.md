## WfScriptNode
The WfScriptNode class used to receive up to 6 input values and execute script on them.

![WfScriptNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Expression.png)

### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**In0** - **In5** - input values for expression. 

### Output Points

**Result** - The expression result.

### Properties

**string Expression** - indicates specified value type: Decimal, Boolean, String.

### Script Example
Here is the script example which makes substration operation on two input values;

```csharp
double topBid = (double)In1;
double bottomAsk = (double)In0;

double spredToOpen = topBid - bottomAsk;
return spredToOpen;
```

User can create and edit expression in special script editor, in Visual Designer. 

![Script Editor](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/VisualDesigner/ScritpEditor.png)

