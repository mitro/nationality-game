using System;
using System.Windows;
using FluentAssertions;
using Moq;
using NationalityGame.Mechanics;
using NationalityGame.Mechanics.Domain;
using NationalityGame.Presentation.Interactivity;
using NationalityGame.Presentation.Views;
using NationalityGame.Tests.Base;
using Xunit;

namespace NationalityGame.Presentation.Tests.Unit
{
    public class GamePresenterTests
    {
        [Fact]
        public void Should_throw_if_already_started_in_start()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            Action secondStartAction = () => presenter.Start();

            secondStartAction.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Should_show_bucket_views_in_start()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IBucketView>()
                .Verify(v => v.Show());
        }

        [Fact]
        public void should_start_new_game_round_in_start()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Verify(g => g.StartNewRound(), Times.Once);
        }

        [Fact]
        public void Should_start_ticker_in_start()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<ITicker>()
                .Verify(t => t.Start());
        }

        [Fact]
        public void Should_refresh_photo_view_on_engine_tick_processed()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Raise(e => e.TickProcessed += null);

            env.Dependency<IPhotoView>()
                .Verify(v => v.Refresh(), Times.Once);
        }

        [Fact]
        public void Should_stop_ticker_on_game_round_finished()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Raise(e => e.RoundFinished += null, 100);

            env.Dependency<ITicker>()
                .Verify(t => t.Stop(), Times.Once);
        }

        [Fact]
        public void Should_hide_photo_view_on_game_round_finished()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Raise(e => e.RoundFinished += null, 100);

            env.Dependency<IPhotoView>()
                .Verify(v => v.Hide(), Times.Once);
        }

        [Fact]
        public void Should_show_game_result_view_on_game_round_finished()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Raise(e => e.RoundFinished += null, 100);

            env.Dependency<IGameResultView>()
                .Verify(v => v.Show(100), Times.Once);
        }

        [Fact]
        public void Should_make_engine_process_pan_on_pan_recognized()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            var vector = new Vector();

            env.Dependency<IGestureRecognizer>()
                .Raise(e => e.PanRecognized += null, vector);

            env.Dependency<IGameEngine>()
                .Verify(e => e.ProcessPan(vector), Times.Once);
        }

        [Fact]
        public void Should_hide_game_result_view_on_play_again_requested()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameResultView>()
                .Raise(v => v.PlayAgainRequested += null);

            env.Dependency<IGameResultView>()
                .Verify(v => v.Hide(), Times.Once);
        }

        [Fact]
        public void Should_start_new_round_on_play_again_requested()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameResultView>()
                .Raise(v => v.PlayAgainRequested += null);

            env.Dependency<IGameEngine>()
                .Verify(e => e.StartNewRound(), Times.Exactly(2));
        }

        [Fact]
        public void Should_start_ticker_on_play_again_requested()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameResultView>()
                .Raise(v => v.PlayAgainRequested += null);

            env.Dependency<ITicker>()
                .Verify(t => t.Start(), Times.Exactly(2));
        }

        [Fact]
        public void Should_make_engine_process_tick_on_ticked()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<ITicker>()
                .Raise(v => v.Ticked += null, 12);

            env.Dependency<IGameEngine>()
                .Verify(e => e.ProcessTick(12), Times.Once);
        }

        [Fact]
        public void Should_make_photo_view_fading_out_on_bucket_chosen()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            env.Dependency<IGameEngine>()
                .Raise(e => e.BucketChosen += null, 15);

            env.Dependency<IPhotoView>()
                .Verify(v => v.StartFadingOut(15), Times.Once);
        }

        [Fact]
        public void Should_show_photo_view_on_next_photo_run()
        {
            var env = new TestEnv();

            var presenter = env.Setup();

            presenter.Start();

            var photo = new Photo(new Point(), string.Empty, string.Empty);

            env.Dependency<IGameEngine>()
                .Raise(e => e.NextPhotoRun += null, photo);

            env.Dependency<IPhotoView>()
                .Verify(v => v.Show(photo), Times.Once);
        }

        private class TestEnv : BaseTestEnv<GamePresenter>
        {
            protected override void SetupDependencies()
            {
                AddDependency<IGameEngine>();

                AddDependency<IPhotoView>();

                AddDependency<IGameResultView>();

                AddDependency<IBucketView>();

                AddDependency<ITicker>();

                AddDependency<IGestureRecognizer>();
            }
        }
    }
}