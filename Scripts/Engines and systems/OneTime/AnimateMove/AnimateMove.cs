using System;
using Server.OneTime.Events;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Mobiles;

namespace Server.AnimateMove
{
    static class AnimateMove
    {
        private static int MainCounter;

        public static void Initialize()
        {
            MainCounter = 0;

            OneTimeMilliEvent.MilliTimerTick += UpdateMove;

            //OneTimeMilliEvent.MilliTimerTick += async (sender, e) => await UpdateMoveAsync(); //Async Event Register (example w/method below)
        }

        private static void UpdateMove(object o, EventArgs e)
        {
            MainCounter++;

            if (MainCounter > 100)
            {
                MainCounter = 0;

                List<Item> Items = GetAnimatedItems();

                foreach (Item item in Items)
                {
                    AnimateItem(item);
                }
            }
        }

        //private static Task UpdateMoveAsync()
        //{
        //    MainCounter++;

        //    if (MainCounter > 100)
        //    {
        //        MainCounter = 0;

        //        List<Item> Items = GetAnimatedItems();

        //        foreach (Item item in Items)
        //        {
        //            AnimateItem(item);
        //        }
        //    }
        //    return Task.CompletedTask;
        //}

        private static void AnimateItem(Item item)
        {
            IAnimateMove _item = item as IAnimateMove;

            if (_item.IsRunning)
            {
                bool IsTime = MoveSpeed(item);

                if (IsTime)
                {
                    if (_item.shadow != null)
                        _item.shadow.DeleteShadow();

                    MovePlayer(item);

                    StartMove(item, _item.MoveDirection);

                    SpawnShadow(item);
                }
            }
        }

        private static List<Item> items = new List<Item>();

        private static List<Item> GetAnimatedItems()
        {
            if (items.Count > 0)
                items.Clear();

            foreach (Item item in World.Items.Values)
            {
                if (item is IAnimateMove)
                    items.Add(item);
            }
            return items;
        }

        private static Direction GetDirection(int direct)
        {
            if (direct == 1)
                return Direction.ValueMask;
            if (direct == 2)
                return Direction.Up;
            if (direct == 3)
                return Direction.North;
            if (direct == 4)
                return Direction.Right;
            if (direct == 5)
                return Direction.East;
            if (direct == 6)
                return Direction.ValueMask;
            if (direct == 7)
                return Direction.Down;
            if (direct == 8)
                return Direction.South;
            if (direct == 9)
                return Direction.Left;
            if (direct == 10)
                return Direction.West;

            return Direction.ValueMask;
        }

        private static bool MoveSpeed(Item item)
        {
            IAnimateMove _item = item as IAnimateMove;

            _item.MoveCount--;

            if (_item.MoveCount > 0)
            {
                return false;
            }
            else
            {
                _item.MoveCount = _item.MoveSpeed;

                return true;
            }
        }
        
        private static void MovePlayer(Item item)
        {
            IAnimateMove _item = item as IAnimateMove;

            if (_item.PM != null && _item.PM.Count > 0)
            {
                _item.PM.Clear();
            }

            IPooledEnumerable pool = item.GetMobilesInRange(0);

            foreach (Mobile player in pool)
            {
                if (player.Z >= item.Z && player.Z <= (item.Z + 5))
                {
                    if (_item.PM != null)
                        _item.PM.Add(player as PlayerMobile);
                }
            }

            pool.Free();
        }

        private static void StartMove(Item item, int dir)
        {
            IAnimateMove _item = item as IAnimateMove;

            int opDir;

            if (dir < 6)
            {
                opDir = (dir + 5);
            }
            else
            {
                opDir = (dir - 5);
            }

            if (_item.MoveForward)
            {
                if (_item.MoveCounter > 0)
                {
                    MoveDir(item, GetDirection(dir));

                    _item.MoveCounter--;
                }
                else
                {
                    _item.MoveForward = false;
                }
            }
            else
            {
                if (_item.MoveCounter < _item.MoveDistance)
                {
                    MoveDir(item, GetDirection(opDir));

                    _item.MoveCounter++;
                }
                else
                {
                    _item.MoveForward = true;
                }
            }
        }

        public static void SpawnShadow(Item item)
        {
            IAnimateMove _item = item as IAnimateMove;

            int getZ = GetGround(item);

            if (getZ != 999)
            {
                Point3D loc = new Point3D(item.X, item.Y, getZ);

                _item.shadow = new Shadow(item.ItemID, item, _item.MoveSpeed);

                _item.shadow.MoveToWorld(loc, item.Map);
            }
        }

        private static void MoveDir(Item item, Direction direction)
        {
            IAnimateMove _item = item as IAnimateMove;

            if (_item.MoveDirection == 1)
            {
                if (_item.MoveForward)
                    item.Z++;
                else
                    item.Z--;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        if (_item.MoveForward)
                            pm.Z++;
                        else
                            pm.Z--;
                    }
                }
            }

            if (direction == Direction.Up)
            {
                item.X--;
                item.Y--;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X--;
                        pm.Y--;
                    }
                }
            }

            if (direction == Direction.North)
            {
                item.Y--;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.Y--;
                    }
                }
            }

            if (direction == Direction.Right)
            {
                item.X++;
                item.Y--;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X++;
                        pm.Y--;
                    }
                }
            }

            if (direction == Direction.East)
            {
                item.X++;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X++;
                    }
                }
            }

            if (_item.MoveDirection == 6)
            {
                if (_item.MoveForward)
                    item.Z--;
                else
                    item.Z++;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        if (_item.MoveForward)
                            pm.Z--;
                        else
                            pm.Z++;
                    }
                }
            }

            if (direction == Direction.Down)
            {
                item.X++;
                item.Y++;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X++;
                        pm.Y++;
                    }
                }
            }

            if (direction == Direction.South)
            {
                item.Y++;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.Y++;
                    }
                }
            }

            if (direction == Direction.Left)
            {
                item.X--;
                item.Y++;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X--;
                        pm.Y++;
                    }
                }
            }

            if (direction == Direction.West)
            {
                item.X--;

                if (_item.PM != null && _item.PM.Count > 0)
                {
                    foreach (PlayerMobile pm in _item.PM)
                    {
                        pm.X--;
                    }
                }
            }
        }

        public static int GetGround(Item item)
        {
            LandTile landTile = item.Map.Tiles.GetLandTile(item.X, item.Y);

            StaticTile[] StaticList = item.Map.Tiles.GetStaticTiles(item.X, item.Y);

            Point3D loc = item.Location;

            IPooledEnumerable getItem = item.Map.GetItemsInRange(loc, 0);

            int HighZ = -100;

            foreach (Item FoundItem in getItem)
            {
                if (FoundItem == item)
                {

                }
                else
                {
                    if (landTile.Z < item.Z)
                    {
                        if (item.Z > HighZ)
                        {
                            HighZ = (item.Z - 1);
                        }
                    }
                }
            }

            getItem.Free();

            foreach (StaticTile tile in StaticList)
            {
                if (tile.Z > HighZ)
                {
                    if (tile.Z < item.Z)
                        HighZ = tile.Z;
                }
            }

            if (landTile.Z < item.Z)
            {
                if (landTile.Z > HighZ)
                {
                    return landTile.Z;
                }
                else
                {
                    return HighZ;
                }
            }
            else
                return 999;
        }
    }
}
