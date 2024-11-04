import * as readline from 'node:readline/promises';

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

type WeatherForecast = {
    date: Date,
    temperatureC: number,
    temperatureF: number,
    summary?: string
};

interface RequestData {
    readonly start: number;
    readonly count?: number;
};

class WeatherForecastService {
    constructor(private url: string) {
    }

    async getWeatherForecast(requestData: RequestData): Promise<WeatherForecast[]> {
        let path = this.url + `?start=${requestData.start}&count=${requestData.count ?? 5}`;
        let response = await fetch(path, {
            method: "GET"
        });

        return <WeatherForecast[]>(await response.json());
    }
}

class App {
    service = new WeatherForecastService("http://localhost:5122/weatherforecast");
    
    async run() {
        while (true) {
            let start = await rl.question("Start from: ");
            let count = await rl.question("Count: ");

            let data = await this.service.getWeatherForecast({
                start: Number.parseInt(start),
                count: Number.parseInt(count)
            });

            console.log(data);

            let average = data.map(w => w.temperatureC).reduce((previous, current) => previous + current, 0) / data.length;

            console.log(`Average TempC: ${average}`);
        }
    }
}

let app: App = new App();

(async () => {
    await app.run();
})();
