
import math

class Entry:

    def __init__( self ):

        self.quadrant = None

    def __repr__( self ):

        return "%2d:%2d" % ( self.quadrant, self.distance )

    def apply_geometric_distance( self ):

        self.distance = math.floor( math.sqrt ( self.x**2 + self.y**2  ) )
    
    def apply_linear_distance( self ):

        abs_x = math.fabs( self.x )
        abs_y = math.fabs( self.y )

        if( abs_y > abs_x ):    self.distance = abs_y
        else:                   self.distance = abs_x

    def apply_quadrant( self, n ):

        circ        = math.pi * 2
        theta       = self.theta
        peak        = n * 2
        quadrant    = 0

        for i in xrange( 1, peak, 2 ):
            if( theta < circ * ( i / float(peak) )):
                self.quadrant = quadrant
                break
            quadrant += 1

        if( self.quadrant == None ):
            self.quadrant = 0

class Matrix:

    def __init__( self, radius ):

        self.radius     = radius
        self.diameter   = radius*2
        self.entries    = []

        for i in xrange( self.diameter+1 ):

            embedded = []

            for i in xrange( self.diameter+1 ):

                embedded.append( 0 )

            self.entries.append( embedded )

        for y in xrange( -radius, radius+1 ):

            for x in xrange( -radius, radius+1 ):

                if   ( y == 0 and x < 0 ):  theta = math.pi / 2 
                elif ( y == 0 and x > 0 ):  theta = math.pi / 2 + math.pi
                elif ( y < 0 and x == 0 ):  theta = 0
                elif ( y > 0 and x == 0 ):  theta = math.pi
                elif ( x == 0 and y == 0 ): theta = 0
                else:

                    val = float(x) / float(y)
                    theta = math.atan( val ); 

                    if( x < 0 and y < 0 ):      pass
                    elif( x < 0 and y > 0 ):    theta += math.pi
                    elif( x > 0 and y > 0 ):    theta += math.pi
                    elif( x > 0 and y < 0 ):    theta += math.pi * 2

                entry = Entry()

                distance = math.sqrt ( x**2 + y**2  )

                entry.x = x
                entry.y = y
                entry.theta = theta

                self.entries[y+radius][x+radius] = entry

    def dump( self ):

        for y in xrange( -self.radius, self.radius+1 ):
            for x in xrange( -self.radius, self.radius+1 ):
                print `self.entries[y+self.radius][x+self.radius]`,
            print ""

    def apply_geometric_distance( self ):

        for y in xrange( -self.radius, self.radius+1 ):
            for x in xrange( -self.radius, self.radius+1 ):
                self.entries[y+self.radius][x+self.radius].apply_geometric_distance()

    def apply_linear_distance( self ):

        for y in xrange( -self.radius, self.radius+1 ):
            for x in xrange( -self.radius, self.radius+1 ):
                self.entries[y+self.radius][x+self.radius].apply_linear_distance()

    def apply_quadrants( self, n ):

        self.quadrants = n

        for y in xrange( -self.radius, self.radius+1 ):
            for x in xrange( -self.radius, self.radius+1 ):
                self.entries[y+self.radius][x+self.radius].apply_quadrant( 8 )

    def add_entries( self, set ):

        for y in xrange( -self.radius, self.radius+1 ):
            for x in xrange( -self.radius, self.radius+1 ):
                entry = self.entries[y+self.radius][x+self.radius]
                if( entry.distance != 0 ): set.add_entry( entry )

class EntrySet:
    
    def __init__( self, distance ):

        self.distance = distance
        self.entries = []

    def add_entry( self, entry ):

        print "(2) adding entry " + `entry`
        self.entries.append( entry )
    
    def __repr__( self ):

        return `self.entries`

class Quadrant: 

    def __init__( self, quadrant ):

        self.quadrant = quadrant
        self.index = {}
        self.entries = []

    def __repr__( self ):

        return `self.entries`        

    def add_entry( self, entry ):

        print "(1) adding entry " + `entry`
        if( entry.distance in self.index ):

            set = self.index[entry.distance] 
    
        else:
            
            set = EntrySet( entry.distance )
            self.index[entry.distance] = set

        set.add_entry( entry )

    def sort( self ):


        print "copying %d entries" % len( self.index )
        for value in self.index.values():
            self.entries.append( value )

        def cmp( one, two ):

            if( one.distance <  two.distance ): return -1
            elif( one.distance >  two.distance ): return 1
            else: return 0

        self.entries.sort( cmp )

class SearchSet:

    def __init__( self, n_quadrants ):
        
        self.n_quadrants = n_quadrants
        self.quadrants = []

        for i in xrange( n_quadrants ):

            self.quadrants.append( Quadrant( i ) )

    def add_entry( self, entry ):

        self.quadrants[entry.quadrant].add_entry( entry )

    def dump( self ):

        for quadrant in self.quadrants:

            print quadrant
            print

    def sort( self ):

        for quadrant in self.quadrants:

            quadrant.sort( )

    def __repr__( self ):

        return `self.quadrants`

def main( ):

    matrix = Matrix( 24 )
    n_quadrants = 8

    matrix.apply_geometric_distance( )
    matrix.apply_quadrants( n_quadrants )
    set = SearchSet( n_quadrants )
    matrix.add_entries( set )
    ##matrix.dump( )
    set.sort()
    set.dump()

    codegen(set)

def codegen( set ):

    print "//------------------------------------------------------------------------------"
    print "//  WARNING--WARNING--THIS IS GENERATED CODE; DO NOT EDIT!!--WARNING--WARNING   "
    print "//------------------------------------------------------------------------------"
    print "using System;"
    print "using Server;"
    print "//------------------------------------------------------------------------------"
    print "namespace Server.Misc {"
    print "//------------------------------------------------------------------------------"
    print "public partial class Visitor"
    print "{"
    print "//------------------------------------------------------------------------------"
    print "internal static[,,] m_SearchQuadrants ="
    print "{"


    #for quadrant in set.quadrants:

        

if __name__=="__main__": main()

