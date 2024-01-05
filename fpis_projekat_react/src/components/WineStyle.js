import { Component } from 'react';

class WineStyle extends Component 
{
    constructor(props) 
    {
        super(props);
        this.state = { wineStyles: [], selectedWineStyles: [] }
    }

    componentDidMount() 
    {
        this.refreshWineStyles();
    }

    async refreshWineStyles() 
    {
        fetch("http://localhost:5274/api/WineShop/GetWineStyles")
            .then(response => response.json())
            .then(data => { this.setState({ wineStyles: data }); })
            .catch(error => console.error('Error:', error));
    }

    handleCheckboxChange = (wineStyleId) => 
    {
        const { selectedWineStyles } = this.state;

        const updatedWineStyles = selectedWineStyles.includes(wineStyleId)
            ? selectedWineStyles.filter(id => id !== wineStyleId)
            : [...selectedWineStyles, wineStyleId];

        this.setState({ selectedWineStyles: updatedWineStyles }, () =>
        {
            this.props.onSelectedWineStylesChange(this.state.selectedWineStyles);
        });
    }
    
    render() 
    {
        const{ wineStyles } = this.state;
        const { isOnScreen } = this.props;
        return (
            <section id='wine-style' className={`p-2 sticky-wine-style ${isOnScreen ? 'cart-on-screen' : ''}`}>
                <h5>Stil</h5>
                { wineStyles.map(wineStyle => (
                    <span  key={wineStyle.ID}>
                        <input type="checkbox" id={wineStyle.ID} name={wineStyle.ID} value={wineStyle.Name} 
                            checked={this.state.selectedWineStyles.includes(wineStyle.ID)}
                            onChange={() => this.handleCheckboxChange(wineStyle.ID)}
                        />
                        <label htmlFor={wineStyle.Name}>&nbsp;{wineStyle.Name}</label><br />
                    </span>  
                ))}
            </section>
        );
    }
}

export default WineStyle;