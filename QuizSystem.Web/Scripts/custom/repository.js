var repository = (function () {

    var questions = function (quizId) {

        return (function (id) {
            var options = {
                headers: { 'Content-Type': 'application/json'}
            };
           
            function create(question) {
                options.url = '/UserQuizzes/' + id + '/Questions/CreateQuestion';
                options.data = JSON.stringify(question);
                return httpRequester.post(options);
            }

            function update(question) {
                options.url = '/UserQuizzes/' + id + '/Questions/UpdateQuestion';
                options.data = JSON.stringify(question);
                return httpRequester.post(options);
            }

            function remove(question) {
                options.url = '/UserQuizzes/' + id + '/Questions/RemoveQuestion';
                options.data = JSON.stringify(question);
                return httpRequester.post(options);
            }

            function all() {
                options.url = '/UserQuizzes/' + id + '/Questions/AllQuestions';
                return httpRequester.get(options);
            }

            return {
                create: create,
                update: update,
                remove: remove,
                all: all
            }
        }(quizId))
    }

    var categories = (function () {
        var options = {
            headers: { 'Content-Type': 'application/json' }
        };
        
        function update(category) {
            options.url = '/Administration/Categories/Update';
            options.data = JSON.stringify(category);
            return httpRequester.post(options)
        }

        function remove(category) {
            options.url = '/Administration/Categories/Remove';
            options.data = JSON.stringify(category);
            return httpRequester.post(options)
        }

        return {
            update: update,
            remove: remove
        }
    }());

    var quizzes = (function () {
        var options = {};

        function create(params) {
            options.url = '/UserQuizzes/Create';
            options.data = params;
            options.cache = false;
            return httpRequester.post(options);
        }

        function remove(params) {
            options.url = '/UserQuizzes/Remove';
            options.data = params;
            return httpRequester.post(options);
        }

        function update(params) {
            options.url = '/UserQuizzes/Edit';
            options.data = params;
            return httpRequester.post(options);
        }

        function updateGrid(params) {
            options.url = '/UserQuizzes/UpdateGrid?' + params;
            return httpRequester.get(options);
        }

        return {
            create: create,
            remove: remove,
            update: update,
            updateGrid: updateGrid
        }
    }());

    var allQuizzes = (function () {
        var options = {};

        function updateGrid(params) {
            options.url = '/Quizzes/UpdateGrid?' + params;
            return httpRequester.get(options);
        }

        function vote(params) {
            options.url = '/Quizzes/Vote';
            options.data = params;

            return httpRequester.post(options);
        }

        return {
            updateGrid: updateGrid,
            vote: vote
        }
    }());

    return {
        questions: questions,
        categories: categories,
        quizzes: quizzes,
        allQuizzes: allQuizzes
    }
}());