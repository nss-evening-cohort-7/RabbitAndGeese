using System;
using RabbitAndGeese.Models;
using Xunit;

namespace RabbitAndGeese.Tests
{
    public class SaddlingAGoose
    {
        [Fact]
        public void A_goose_and_saddle_of_the_same_size_should_be_compatible()
        {
            //Arrange
            var bunny = new Rabbit();
            var emotionalState = "Poop-filled Anger";
            var largeGoose = new Goose
            {
                Name = "Tedothy",
                Sex = Sex.Male,
                Size = Size.Large,
                EmotionalState = emotionalState
            };

            var largeSaddle = new Saddle { Size = Size.Large, InUse = false };
            bunny.OwnedSaddles.Add(largeSaddle);
            bunny.OwnedGeese.Add(largeGoose);

            //Act
            bunny.SaddleThatGoose(largeGoose, largeSaddle);

            //Assert
            Assert.Same(largeSaddle, largeGoose.Saddle);
            Assert.True(largeSaddle.InUse);
            Assert.Equal(emotionalState, largeGoose.EmotionalState);
        }

        [Fact]
        public void A_goose_and_saddle_that_are_not_the_same_size_should_not_be_compatible()
        {
            //Arrange
            var bunny = new Rabbit();
            var largeGoose = new Goose { Name = "Tedothy", Sex = Sex.Male, Size = Size.Large };
            var smallSaddle = new Saddle { Size = Size.Small, InUse = false };
            bunny.OwnedSaddles.Add(smallSaddle);
            bunny.OwnedGeese.Add(largeGoose);

            //Act
            bunny.SaddleThatGoose(largeGoose, smallSaddle);

            //Assert
            Assert.NotSame(smallSaddle, largeGoose.Saddle);
            Assert.False(smallSaddle.InUse);
            Assert.Equal("Distraught by your fat shaming", largeGoose.EmotionalState);
        }

        [Fact]
        public void A_goose_that_is_saddled_should_not_accept_another_saddle()
        {
            //Arrange
            var bunny = new Rabbit();
            var emotionalState = "Poop-filled Anger";
            var originalSaddle = new Saddle();
            var largeGoose = new Goose
            {
                Name = "Tedothy",
                Sex = Sex.Male,
                Size = Size.Large,
                EmotionalState = emotionalState,
                Saddle = originalSaddle
            };

            var largeSaddle = new Saddle { Size = Size.Large, InUse = false };
            bunny.OwnedSaddles.Add(largeSaddle);
            bunny.OwnedGeese.Add(largeGoose);

            //Act
            bunny.SaddleThatGoose(largeGoose, largeSaddle);

            //Assert
            //Assert.NotEqual(largeSaddle, largeGoose.Saddle);
            Assert.Equal(originalSaddle, largeGoose.Saddle);
            Assert.Equal("Exhausted",largeGoose.EmotionalState);
        }
    }
}
