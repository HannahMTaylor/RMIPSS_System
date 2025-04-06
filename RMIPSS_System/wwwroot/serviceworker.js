const CACHE_NAME = "RMIPSS-CACHE-V1";
const urlsToCache = [
    "/",
    "/css/site.css",
    "/js/site.js",
    "/offline.html",
    "/Referral/CreateReferralForm"
];

// Install service worker and cache files
self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            return cache.addAll(urlsToCache);
        })
    );
});

// Serve cached content when offline
self.addEventListener("fetch", event => {
    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        }).catch(() => {
            return caches.match("/offline.html");
        })
    );
});

// Activate service worker and remove old caches
self.addEventListener("activate", event => {
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cache => {
                    if (cache !== CACHE_NAME) {
                        return caches.delete(cache);
                    }
                })
            );
        })
    );
});