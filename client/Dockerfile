# Use an official Node.js runtime as the base image
FROM node:18.16.0-alpine

# Set the working directory within the container
WORKDIR /usr/src/app

# Copy package.json and package-lock.json to the working directory
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code into the container
COPY . .

# Build the Next.js application
RUN npm run build

# Expose the port on which the Next.js application will run
EXPOSE 3000

# Start the application when the container is run
CMD ["npm", "start"]
