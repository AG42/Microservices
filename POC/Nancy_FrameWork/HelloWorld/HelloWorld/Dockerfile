FROM mono:4.2.1.102
RUN mkdir /app
COPY . /app
EXPOSE 1234
CMD ["mono", ""/app/HelloWorld.Host.exe", "-d"]