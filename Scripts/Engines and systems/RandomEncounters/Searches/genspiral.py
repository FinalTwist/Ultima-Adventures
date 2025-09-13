
def main( ):

    up      = [ 0, +1 ] # x,y
    right   = [ +1, 0 ] # x,y
    down    = [ 0, -1 ]
    left    = [ -1, 0 ]

    dirs    = [ up, right, down, left ]
    index   = 0
    count   = 0
    length  = 1
    pos     = [0,0]

    print "internal static int[][] m_SpiralPattern = new int[][]"
    print "{"
    for itr in xrange(48):

        for segment in xrange(2):

            index = count % 4
            dir = dirs[index]
            count += 1

            for incr in xrange(length):

                pos[0] = pos[0] + dir[0]
                pos[1] = pos[1] + dir[1]
                print "    new int[] { %3d, %3d }," % ( pos[0], pos[1] )

        length += 1
    print "};"

if __name__=="__main__": main()
