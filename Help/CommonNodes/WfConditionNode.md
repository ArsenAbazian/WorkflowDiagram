### WfConditionNode
The WfConditionNode class used to make conditional operation on two input values and pass execution to the one of the branches: 'True' if condition passed or 'False' if not.

![WfConditionNode](https://github.com/ArsenAbazian/WorkflowDiagram/blob/main/Help/Images/CommonNodes/Conditional.png)

#### Input Points

**Run** - Allows WfRunner to visit the node if the value contained in this point = true, otherwise skips nodes execution.

**In1** - Left input operand

**In2** - Right input operand

#### Output Points

**True** - Connection point for branch if condition passed.

**False** - Connection point for brunch if condition not passed.

#### Properties

**WfConditionalOperation Operation** - specifies the conditional operations: Equal, NotEqual, Less, LessOrEqual, Greater, GreaterOrEqual.
