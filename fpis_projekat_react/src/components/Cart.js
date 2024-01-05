import { Component } from 'react';

class Cart extends Component 
{    
    handleRemoveFromCart = (wineId) => {
        const { selectedWines } = this.props;
        this.props.onRemoveFromCart(selectedWines.filter((wine) => wine.ID !== wineId));
    };

    
    handlePurchase = async () => 
    {
        const { selectedWines } = this.props;
        const { setMessage } = this.props;
        const requestOptions = 
        {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(selectedWines),
        };

        try 
        {
            const response = await fetch('http://localhost:5274/api/WineShop/AddCart', requestOptions);
        
            if (!response.ok) { setMessage(`HTTP error! Status: ${response.status}`); }
        
            const result = await response.json();
            setMessage(result);
            this.props.onRemoveFromCart(selectedWines.filter((wine) => wine.ID === -1));
        } catch (error) 
        {
            setMessage('Servis nije dostupan. Pokušajte kasnije.');
        }
    };

    render() 
    {
        const { total, selectedWines } = this.props;
        
        return (
            <section id='cart' className="p-2">
                <h5>Korpa</h5>
                { selectedWines.sort((a, b) => a.Name.localeCompare(b.Name)).map((selectedWine, index) =>
                    <article className='border border-dark p-2 col-12 my-2' key={selectedWine.ID}>
                        <section className='row'>
                            <p className='col-6'>{index + 1}. {selectedWine.Name}</p>
                            <p className='col-6 text-end'>
                                <button className='btn btn-outline-secondary btn-sm' onClick={() => this.handleRemoveFromCart(selectedWine.ID)}>x</button>
                            </p>
                        </section>
                        <section className='row'>
                            <p className='col-6 m-0'>Količina: {selectedWine.Quantity}</p>
                            <p className='col-6 m-0 text-end'>{selectedWine.TotalPrice} din</p>
                        </section>
                    </article>
                )}
                <div className='p-2 col-12 text-end'>
                    <p className='mb-1'>Ukupno: {total} din</p>
                    <button className='btn btn-outline-secondary text-dark' disabled={total === 0.0} onClick={this.handlePurchase}>Kupi</button>
                </div>
            </section>
        );
    }
}

export default Cart;