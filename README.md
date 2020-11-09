# Example Beijer iX project for Test Driven Development setup with Visual Studio

This is an example implementation to demonstrate the project setup that I came up with in order
to develop Beijer iX software with a Test Driven Development approach in Visual Studio.
I wrote a [blog post](https://timon.la/blog/tdd-for-ix/) on the topic and decided to create an
example project to showcase the setup.

The functionality of the *border-patrol* project is really simple but should provide a good
understanding on how to structure projects with more complex business logic.

## Implementation

* You can choose the `width` and the `height` of a grid (a rectangle)
* If you leave either `width` or `height` as `0`, the grid will be a square
* If you click *Update Grid*, the grid will drawn into the area on the right
* With the *Start/Stop* button you can let the indicator, by the border of the grid,
patrol its border