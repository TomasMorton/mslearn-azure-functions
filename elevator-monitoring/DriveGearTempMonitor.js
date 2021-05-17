const statusFor = (temperature) => {
    if (temperature <= 25) return "OK";
    if (temperature <= 50) return "CAUTION";
    return "DANGER";
};

module.exports = async function (context, req) {
    context.log('Drive Gear Temperature Service triggered');
    if (req.body && req.body.readings) {
        const readings = req.body.readings.map(reading => {
            return {...reading, status: statusFor(reading.temperature) }
        });
        readings.forEach(reading => context.log(`Reading for ${reading.temperature} is ${reading.status}`));
        context.res = {
            // status: 200, /* Defaults to 200 */
            body: {
                "readings": readings
            }
        };
    }
    else {
        context.res = {
            status: 400,
            body: "Please send an array of readings in the request body"
        };
    }
    context.done();

}
