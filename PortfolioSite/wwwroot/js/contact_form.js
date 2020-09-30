class ContactForm extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            name: '',
            email: '',
            subject: '',
            message: '',
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleChange(event) {
        const target = event.target;
        const value = target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    validateName(event) {
        // To implement
    }

    validateEmail(event) {
        // To implement
    }

    validateSubject(event) {
        // To implement
    }

    validateMessage(event) {
        // To implement
    }

    handleSubmit(event) {
        event.preventDefault();
        var contactFormJson = JSON.stringify({ Name: this.state.name, Email: this.state.email, Subject: this.state.subject, Message: this.state.message });

        fetch('https://localhost:44319/Contact/Send',
            {
                method: 'post',
                headers: {
                    'Accept': 'application/json, text/plain, */*',
                    'Content-Type': 'application/json'
                },
                body: contactFormJson
            })
            .then(response => response.json())
            .then(confirmationView => {
                location.href = '/Contact/' + confirmationView;
            }
        );
    }

    render() {
        return (
            <form className="contact-form" autoComplete="off" onSubmit={this.handleSubmit}>
                <label className="contact-label" htmlFor="name">Name</label>
                <input id="name" name="name" type="text" className="contact-item" value={this.state.name} onChange={this.handleChange} />

                <label className="contact-label" htmlFor="email">Email</label>
                <input id="email" name="email" type="text" className="contact-item" value={this.state.email} onChange={this.handleChange} />

                <label className="contact-label" htmlFor="subject">Subject</label>
                <input id="subject" name="subject" type="text" className="contact-item" value={this.state.subject} onChange={this.handleChange} />

                <label className="contact-label" htmlFor="message">Message</label>
                <textarea id="message" name="message" className="contact-item-large" value={this.state.message} onChange={this.handleChange} />

                <div className="contact-submit">
                    <input id="submit" type="submit" value="Submit"/>
                </div>
            </form>
        );
    }
}

ReactDOM.render(
    <ContactForm />,
    document.getElementById('contact_form_container')
);

