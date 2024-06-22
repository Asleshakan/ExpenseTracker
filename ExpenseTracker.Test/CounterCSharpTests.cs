using Microsoft.AspNetCore.Components.Web;
using System;

namespace ExpenseTracker.Test;

/// <summary>
/// These tests are written entirely in C#.
/// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>
public class CounterCSharpTests : TestContext
{
	[Fact]
	public void CounterStartsAtZero()
	{
		// Arrange
		var cut = RenderComponent<Counter>();

		// Assert that content of the paragraph shows counter at zero
		cut.Find("p").MarkupMatches("<p>Current count: 0</p>");
	}

	[Fact]
	public void ClickingButtonIncrementsCounter()
	{
		// Arrange
		var cut = RenderComponent<Counter>();

		// Act - click button to increment counter
		cut.Find("button").Click();

		// Assert that the counter was incremented
		cut.Find("p").MarkupMatches("<p>Current count: 1</p>");
	}

	[Fact]
	public void ClickingButtonIncrementByValueCounterTest()
	{
		var cut = RenderComponent<Counter>(parameters => parameters.Add(x => x.value, 3));
		cut.Find("button").Click();
		cut.Find("p").MarkupMatches("<p>Current count: 1</p>");

	}
	[Fact]
	public void EvenCallbackTest (){
	
		Action<MouseEventArgs> onClickHandle = _ => { };
        Action onSomethingHandle = () => { };

		var cut = RenderComponent<Counter>(parameters =>
		parameters.Add(p => p.OnClick, onClickHandle)
		.Add(o => o.OnSomething, onSomethingHandle)
		);
	}
}
