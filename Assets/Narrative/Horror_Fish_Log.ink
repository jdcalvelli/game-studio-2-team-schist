/*This is the beginning of the untitled horror fishing game. Below will be a series of barks that will play in the text log. The barks will include action lines of what the character is doing - like in A Quiet Room, and it will get progressively stranger as you go through more loops of the gameplay. */
/* Knots are organized by what they are (intro text, reeling text, idle text). Within each knot, lines will be organized by stitches and can be called separately.
Ex: -> reelingTweaked,005 to grab the 5th line in the reelingTweaked knot */

=== introText ===
//Text for when player first starts the game - opening cinematic mood
//Currently 7/13/7 just to make things exciting 
= 01
You awake from a stupor.
Failing to remember it, your dream world slips away.
Replaced by the world ahead.
-> DONE

= 02
You are in a wooden boat.
Ocean encircles you, spilling past eyesight's sharp edge.
The swaying folds your insides.
-> DONE

= 03
The boat holds a fishing rod.
A rusted hook, bucket of bait, and mini cooler.
You are compelled to hook bait.
-> DONE



=== idleNormal ===
//Lines for when it's normal, and they appear 
= 01
Your bait weighs your arm down. 
-> DONE
= 02
The air is stale from the fumes escaping the ocean.
-> DONE
= 03
Your boat rocks unevenly on the waves.
-> DONE
= 04
The cooler twitches in the corner.
-> DONE
= 05
You think you hear a voice in the distance.
-> DONE
= 06
You sharpen your knife against the side of the boat.
-> DONE

=== reelingNormal ===
//Beginning of the game text when reeling a fish
= 01
What's underneath tugs at your line.
-> DONE
= 02
The rod bends and creaks under pressure. 
-> DONE
= 03
A force speeds toward your boat, heavier as seconds pass. 
-> DONE
= 04
Your line thrashes left to right, threatening to pull you over.
-> DONE
= 05
You drag a dead weight through the water.
-> DONE


=== idleUnsettling ===
= 01
The stench from the cooler burns your eyes.
-> DONE
= 02
Blood splatters stain the floorboard.
-> DONE
= 03
You think you hear growls from below.
-> DONE
= 04
You wipe your knife on your sleeve, leaving more red splotches.
-> DONE
= 05
Your mind wanders for a moment.
-> DONE
= 06
You stare at what you caught in the cooler and shudder.
-> DONE

=== reelingUnsettling ===
//After a couple of loops when things start to get stranger, use these lines
= 01
You are not sure what will come up this time.
-> DONE
= 02
What's underneath leaves a trail of blood in the water.
-> DONE
= 03
You cannot reel faster than what swims underneath.
-> DONE
= 04
You prepare your knife for gutting.
-> DONE
= 05
Your back hurts as you pull the weight. 
-> DONE



=== idleTweaked ===
= 01
The water is red and boiling, cooking everything underneath.
-> DONE
= 02
The cooler jerks and rattles in pain.
-> DONE
= 03
The creases in your palm split open as you sharpen your knife.
-> DONE
= 04
You heave the last catch back before throwing up over the boat. 
-> DONE
= 05
You hear screams from below, or from above, or from behind...
-> DONE

=== reelingTweaked ===
//oh fuck oh shit oh no lines
= 01
What's underneath pulls back and begs for life.
-> DONE
= 02
Your shoulders stretch nearly out its socket as you reel.
-> DONE
= 03
You are ready to kill what's underneath.
-> DONE
= 04
You imagine what's underneath bringing you down instead. 
-> DONE
= 05
You crave an end.
-> DONE



=== endText ===
//If we reach an end - and the player is somehow alive(?) this is the text that plays - this is the player "beats the game."
= 01
A strong knife to fleshy guts
But water flowing inside now, filling the gap made
You sink and fall underneath.
-> DONE