import { Component } from 'react';

class Wines extends Component 
{
    constructor(props) 
    {
        super(props);
        this.state = { wines: [], isSet: false }
    }

    componentDidMount() 
    {
        this.refreshWines();
    }

    async refreshWines() 
    {
        if(!this.isSet)
        {
            const { addAllWines } = this.props;
            try 
            {
                const response = await fetch("http://localhost:5274/api/WineShop/GetWines");
                const data = await response.json();
                this.setState({ wines: data }, () => 
                {
                    addAllWines(data);
                    this.isSet = true;
                });
                await this.refreshWines();
                this.props.onWinesLoaded();

            } catch (error) 
            {
                console.error('Error:', error);
            }
        }
    }

    render() 
    {
        const { filteredWines } = this.props;

        return (
            <section id='wines' className="row">
                <h5 className='py-3'>Naša ponuda</h5>
                { filteredWines.map(wine =>
                    <article className='px-1 col-12 col-sm-6 col-md-12 col-lg-6 col-xl-4' key={wine.ID}>
                        <section className='wine mb-2 p-2 text-center'>
                            <img src={`/images/wines/${wine.Name}.jpg`} alt="Wine" width="300px" height="150px" className='img-fluid'></img>
                            <h6 className='mt-2'>{wine.Name}</h6>
                            <section className='row'>
                                <p className='wine-style col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-truncate text-start'>{wine.WineStyle.Name}</p>
                                <p className='col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-end'>{wine.Price} din</p>
                            </section>
                            <section className='row'>
                                <p className='col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-start'>{wine.WineSort.Name}</p>
                                <p className='col-6 col-sm-6 col-md-6 col-lg-6 col-xl-6 text-end'>
                                    <button className='btn btn-outline-secondary' onClick={() => this.props.onAddToCart(wine)}>+</button>
                                </p>
                            </section>
                        </section>
                    </article>
                )}
            </section>
        );
    }
}

export default Wines;