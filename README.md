# Example Beijer iX project for Test Driven Development setup with Visual Studio

![Demo recording](/assets/demo.gif)

This is an example implementation to demonstrate the project setup that I came up with in order
to develop Beijer iX software with a Test Driven Development approach in Visual Studio.
I decided to create this example implementation for my [blog post](https://timon.la/blog/tdd-for-ix/)
on the topic.

The functionality of the *border-patrol* project is really simple but should provide a good
understanding on how to structure projects with more complex business logic.

## Functionality

* You can choose the `width` and the `height` of a grid (a rectangle)
* If you leave either `width` or `height` as `0`, the grid will be a square
* If you click *Update Grid*, the grid will drawn into the area on the right
* With the *Start/Stop* button you can let the indicator, by the border of the grid,
patrol its border
