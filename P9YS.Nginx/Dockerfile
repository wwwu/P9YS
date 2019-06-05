FROM nginx
ARG env
COPY P9YS.Nginx/p9ys_$env.conf /etc/nginx/conf.d/p9ys.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]