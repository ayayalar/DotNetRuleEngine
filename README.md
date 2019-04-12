> DotNetRuleEngine allows you to write your code as series of rules to keep your code clean and structured. Supports both **synchronous** and **asynchronous** patterns and it adheres to **S.O.L.I.D** design principles.


### A few reasons use DotNetRuleEngine ###
- Adheres to S.O.L.I.D
- Testable code.
- Encapsulates varying behavior. Such as business rules.

```csharp
PM> Install-Package DotNetRuleEngine
```
Nuget package available at: [DotNetRuleEngine](https://www.nuget.org/packages/DotNetRuleEngine "DotNetRuleEngine")

Get Started at: [DotNetRuleEngine Wiki](https://github.com/ayayalar/DotNetRuleEngine/wiki)

[![Build Status](https://dev.azure.com/ayayalar/ayayalar/_apis/build/status/ayayalar.DotNetRuleEngine?branchName=master)](https://dev.azure.com/ayayalar/ayayalar/_build/latest?definitionId=1&branchName=master)

## Example

#### Model

```csharp
public class Order
{
    public int Id { get; set; }
    public decimal Total { get; set; }
    public bool FreeShipping { get; set; }
}

Order order = new Order { Id = 1, Total = 79.99 };
```

#### Install DotNetRuleEngine
```install-package dotnetruleengine```


#### Create Rule(s)

*Create a rule to update FreeShipping attribute if the amount is greater than $50.00*

```csharp
public class QualifiesForFreeShipping: Rule<Order>
{   
    public override IRuleResult Invoke()
    {
        if (Model.Total > 50.0m)
        {
            Model.FreeShipping = true;
        }
        
        return null;
    }
}
```

#### Invoke Rule(s)

```csharp    
var ruleResults = RuleEngine<Order>.GetInstance(order)
    .ApplyRules(new QualifiesForFreeShipping())
    .Execute()
```
