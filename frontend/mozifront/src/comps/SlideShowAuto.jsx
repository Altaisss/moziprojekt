import React, { useState, useEffect } from "react"
import '../css/Slideshow.css'
import popcorn from '../images/popcorn.jpg'
import seating from '../images/seating.png'

function SlideshowAuto() {

    //Array of images >>>
    const images = [
        popcorn,
        seating      
    ]

    //Our State >>>
    const [nextIndex, setNextIndex] = useState(0);

    //Update "images" to your array name >>>
    const arrayLength = images.length;

    //Our setTimeout method >>>
    useEffect(() => {
        const interval = setInterval(() => {
            setNextIndex(prev => (prev + 1) % images.length);
        }, 3000);

        return () => clearInterval(interval); // cleanup on unmount
    }, []); // empty array = runs once on mount


    return (
        <div id="slideshow-container">
            <div id="slideshow-image">
                <img style={{ width: 500 }} src={images[nextIndex]} alt="pic" ></img>
            </div>
        </div>
    )
}

export default SlideshowAuto;