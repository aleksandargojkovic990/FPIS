import { Component } from 'react';

class WineSort extends Component 
{
    constructor(props) 
    {
        super(props);
        this.state = { wineSorts: [], selectedWineSorts: [] };
    }
    
    componentDidMount() 
    {
        this.refreshWineSorts();
    }
    
    async refreshWineSorts() 
    {
        try 
        {
            const response = await fetch("http://localhost:5274/api/WineShop/GetWineSorts");
            const data = await response.json();

            this.setState({ wineSorts: data });
        } catch (error) {
            console.error('Error:', error);
        }
    }

    handleCheckboxChange = (wineSortId) => 
    {
        const { selectedWineSorts } = this.state;

        const updatedWineSorts = selectedWineSorts.includes(wineSortId)
            ? selectedWineSorts.filter(id => id !== wineSortId)
            : [...selectedWineSorts, wineSortId];

        this.setState({ selectedWineSorts: updatedWineSorts }, () =>
        {
            this.props.onSelectedWineSortsChange(this.state.selectedWineSorts);
        });
    }

    render() 
    {
        const { wineSorts } = this.state;
        const { isOnScreen } = this.props;
        return (
            <section id='wine-sort' className={`p-2 my-2 sticky-wine-sort ${isOnScreen ? 'cart-on-screen' : ''}`}>
                <h5>Sorta</h5>
                { wineSorts.map(wineSort => (
                    <span key={wineSort.ID}>
                        <input type="checkbox" id={wineSort.ID} name={wineSort.ID} value={wineSort.Name}
                            checked={this.state.selectedWineSorts.includes(wineSort.ID)}
                            onChange={() => this.handleCheckboxChange(wineSort.ID)}
                        />
                        <label htmlFor={wineSort.Name}>&nbsp;{wineSort.Name}</label><br />
                    </span>
                ))}
            </section>
        );
    }
}
    
export default WineSort;