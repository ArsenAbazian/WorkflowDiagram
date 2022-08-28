## WfRepeatNode
The WfRepeatNode class used to repeat sub-branch N times, specified in the Count property. 
Each time in loop, object in 'In' connection point passed to the sub-branch, connected to the 'Repeat' connection point, executes and execution result adds to the collection object, which contained in the 'Out' output connection point.

![WfRepeatNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Repeat.png)

### Input Points

**In** - Input object.

### Output Points

**Out** - Contains result collection object.

**Repeat** - Branch, which will be executed N times, specified in the 'Count' property

### Properties

**int Count** - Specifies loop count

