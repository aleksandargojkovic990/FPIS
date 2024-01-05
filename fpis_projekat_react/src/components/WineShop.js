import React, { useState, useEffect, useRef } from 'react';
import WineStyle from './WineStyle';
import WineSort from './WineSort';
import Wines from './Wines';
import Cart from './Cart';

function WineShop() 
{
    const [selectedWineStyles, setSelectedWineStyles] = useState([]);
    const [selectedWineSorts, setSelectedWineSorts] = useState([]);
    const [allWines, setAllWines] = useState([]);
    const [filteredWines, setFilteredWines] = useState([]);
    const [selectedWines, setSelectedWines] = useState([]);
    const [total, setTotal] = useState(0.0);
    const [isOnScreen, setIsOnScreen] = useState(false);
    const cartRef = useRef(null);
    const [message, setMessage] = useState('');
    const [isWinesLoaded, setIsWinesLoaded] = useState(false);

    const handleSelectedWineStylesChange = (newSelectedWineStyles) => {
        setSelectedWineStyles(newSelectedWineStyles);
    };

    const handleSelectedWineSortsChange = (newSelectedWineSorts) => {
        setSelectedWineSorts(newSelectedWineSorts);
    };

    const handleWinesLoaded = () => {
        setIsWinesLoaded(true);
    };

    const handleAddAllWines = (allWiness) => {
        setAllWines(allWiness);
    }

    useEffect(() => {
        filterWines();
    }, [selectedWineStyles, selectedWineSorts, allWines]);

    useEffect(() => {
        if(filteredWines.length === 0 && isWinesLoaded)
        {
            setMessage('Za zadate parametre ne postoje vina.');
        }
    }, [filteredWines]);

    const filterWines = () => {
        const isEmptySelectedWineSorts = !selectedWineSorts || selectedWineSorts.length === 0;
        const isEmptySelectedWineStyles = !selectedWineStyles || selectedWineStyles.length === 0;

        setFilteredWines(allWines.filter(wine =>
            ((isEmptySelectedWineSorts || selectedWineSorts.includes(wine.WineSort.ID))
                && (isEmptySelectedWineStyles || selectedWineStyles.includes(wine.WineStyle.ID)))));
    }

    const handleAddToCart = (wine) => {
        const existingWineIndex = selectedWines.findIndex((cartWine) => cartWine.ID === wine.ID);
      
        if (existingWineIndex !== -1) 
        {
            const updatedWines = selectedWines.map((cartWine, index) =>
                index === existingWineIndex ? { ...cartWine, Quantity: cartWine.Quantity + 1, TotalPrice: cartWine.Price * (cartWine.Quantity + 1) } : cartWine
            );
            setSelectedWines(updatedWines);
        } else 
        {
            const newWine = { ...wine, Quantity: 1, TotalPrice: wine.Price };
            setSelectedWines((prevSelectedWines) => [...prevSelectedWines, newWine]);
        }

        handleSetMessage(`${wine.Name} je dodato u korpu!`);
    };

    const handleRemoveFromCart = (updatedWines) => {
        setSelectedWines(updatedWines);
        handleSetMessage(`Vino je izbačeno iz korpe!`);
    };

    const handleSetMessage = (msg) => {
        setMessage(msg);
    };
    
    useEffect(() => {
        const newTotal = selectedWines.reduce((acc, currWine) => acc + currWine.TotalPrice, 0);
        setTotal(newTotal);
    }, [selectedWines]);

    useEffect(() => {
        const handleIntersection = (entries) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    setIsOnScreen(true);
                  } else {
                    setIsOnScreen(false);
                  }
            });
        };

        const options = {
            root: null,
            rootMargin: '0px',
            threshold: 0.1,
        };

        const observer = new IntersectionObserver(handleIntersection, options);

        if (cartRef.current) {
            observer.observe(cartRef.current);
        }

        return () => {
            if (cartRef.current) {
                observer.unobserve(cartRef.current);
            }
        };
    }, []);

    useEffect(() => {
        const timeoutId = setTimeout(() => {
          setMessage('');
        }, 2000);
    
        return () => clearTimeout(timeoutId);
      }, [message]);

    return (
        <div className="row">
            <h4 className='py-4 border-bottom'>FONdrum vina</h4>
            <section className="col-12 col-sm-12 col-md-6 col-lg-4 col-xl-3">
                <WineStyle onSelectedWineStylesChange={handleSelectedWineStylesChange} isOnScreen={isOnScreen} />
                <WineSort onSelectedWineSortsChange={handleSelectedWineSortsChange} isOnScreen={isOnScreen} />
            </section>
            <section className="col-12 col-sm-12 col-md-6 col-lg-8 col-xl-6">
                <Wines onWinesLoaded={handleWinesLoaded}
                    addAllWines={handleAddAllWines}
                    filteredWines={filteredWines}
                    onAddToCart={handleAddToCart} />
            </section>
            <section id="cart-holder" className="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-3" ref={cartRef}>
                <Cart selectedWines={selectedWines} 
                    total={total} 
                    onRemoveFromCart={handleRemoveFromCart}
                    setMessage={handleSetMessage}
                />
            </section>
            {
                message && 
                <div className="message-container">
                    <div className="message">{message}</div>
                </div>
            }
        </div>
    );
}

export default WineShop;