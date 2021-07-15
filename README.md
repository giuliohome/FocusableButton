# FocusableButton
=======
Question https://stackoverflow.com/q/68387786/11323942


Original Question
I create a new WPF .NET Framework (for example 4.8) project in C#.

I use my opensource nuget package GiulioMVVM to get a standard implementation of property changed, but whatever you prefer it is irrelevant to the question.

In the XAML designer I introduce a TextBox, a Button and a TextBlock.

In the ViewModel I define 2 standard PropertyA and PropertyB in the usual way
```
private double myVarA; 
public double MyPropertyA
{
    get { return myVarA; }
    set { myVarA = value; OnPropertyChanged(); }
}
```
and I set the bindings
```
 <TextBox Text="{Binding MyPropertyA}"
 
 <TextBlock Text="{Binding MyPropertyB}"
```
and
```
 <Button Focusable="False" Command="{Binding DoubleCmd}"
```
where the trivial command is
```
DelegateCommand doubleCmd;
public ICommand DoubleCmd
{
    get
    {
        if (doubleCmd is null)
        {
            doubleCmd = new DelegateCommand(_ =>
                MyPropertyB = MyPropertyA * 2
            );
        }
        return doubleCmd;
    }
}
```
Then I start testing and I see that, if I change a value and I immediately click the button, the value is not correctly refreshed.

I think that to fix it I have to set
```
<TextBox Text="{Binding MyPropertyA, UpdateSourceTrigger=PropertyChanged}"

(then maybe, to handle a double binding better than TextBox I have to choose the Extended Toolkit with a DoubleUpDown Control)
```
Is my reasoning correct? Or is it an XY problem?

## Answer

> I think that to fix it I have to set
```
<TextBox Text="{Binding MyPropertyA, UpdateSourceTrigger=PropertyChanged}"
```
> Is my reasoning correct?

No, you could be completely wrong here, devil hides in details and you're maybe overlooking a fundamental detail.

You can make the button focusable, why are you setting Focusable="False"?

Is it really needed?

Can't you simply delete it?

Indeed that is the solution. The fact is that the real requirement is not to raise a notification while the user is typing a double, that would not make sense since the number is not even fully typed.

The real requirement is to consider the click of the button as lost focus event. Setting the button as not focusable is therefore simply wrong.
