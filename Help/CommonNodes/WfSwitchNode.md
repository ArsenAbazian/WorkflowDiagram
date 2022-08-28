## WfSwitchNode

The WfSwitchNode class is used to compare input value with the list of values and passes execution to one of the branches connected to the point with the matched value.

![WfSwitchNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Switch.png)

### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**In** - Input value

### Output Points

**Default** - brunch connected to this point will be executed if the input value is matched with none the value from the values list.

This node supports additional custom output connection points. Users can add custom output points and assign matching values for them. On picture above, 1 and 2 output connection points were added by user.


