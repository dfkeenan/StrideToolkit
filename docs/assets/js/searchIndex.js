
var camelCaseTokenizer = function (builder) {

  var pipelineFunction = function (token) {
    var previous = '';
    // split camelCaseString to on each word and combined words
    // e.g. camelCaseTokenizer -> ['camel', 'case', 'camelcase', 'tokenizer', 'camelcasetokenizer']
    var tokenStrings = token.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
      var current = cur.toLowerCase();
      if (acc.length === 0) {
        previous = current;
        return acc.concat(current);
      }
      previous = previous.concat(current);
      return acc.concat([current, previous]);
    }, []);

    // return token for each string
    // will copy any metadata on input token
    return tokenStrings.map(function(tokenString) {
      return token.clone(function(str) {
        return tokenString;
      })
    });
  }

  lunr.Pipeline.registerFunction(pipelineFunction, 'camelCaseTokenizer')

  builder.pipeline.before(lunr.stemmer, pipelineFunction)
}
var searchModule = function() {
    var documents = [];
    var idMap = [];
    function a(a,b) { 
        documents.push(a);
        idMap.push(b); 
    }

    a(
        {
            id:0,
            title:"PrefabExtensions",
            content:"PrefabExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/PrefabExtensions',
            title:"PrefabExtensions",
            description:""
        }
    );
    a(
        {
            id:1,
            title:"ListStackExtensions",
            content:"ListStackExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Collections/ListStackExtensions',
            title:"ListStackExtensions",
            description:""
        }
    );
    a(
        {
            id:2,
            title:"RandomExtensions",
            content:"RandomExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Mathematics/RandomExtensions',
            title:"RandomExtensions",
            description:""
        }
    );
    a(
        {
            id:3,
            title:"CollectionExtensions",
            content:"CollectionExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Collections/CollectionExtensions',
            title:"CollectionExtensions",
            description:""
        }
    );
    a(
        {
            id:4,
            title:"ModelComponentExtensions",
            content:"ModelComponentExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/ModelComponentExtensions',
            title:"ModelComponentExtensions",
            description:""
        }
    );
    a(
        {
            id:5,
            title:"CameraExtensions",
            content:"CameraExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/CameraExtensions',
            title:"CameraExtensions",
            description:""
        }
    );
    a(
        {
            id:6,
            title:"EntityComponentCollectionExtensions",
            content:"EntityComponentCollectionExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/EntityComponentCollectionExtensions',
            title:"EntityComponentCollectionExtensions",
            description:""
        }
    );
    a(
        {
            id:7,
            title:"MathUtilEx",
            content:"MathUtilEx",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Mathematics/MathUtilEx',
            title:"MathUtilEx",
            description:""
        }
    );
    a(
        {
            id:8,
            title:"GameExtensions",
            content:"GameExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/GameExtensions',
            title:"GameExtensions",
            description:""
        }
    );
    a(
        {
            id:9,
            title:"EntityExtensions",
            content:"EntityExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/EntityExtensions',
            title:"EntityExtensions",
            description:""
        }
    );
    a(
        {
            id:10,
            title:"EntityComponentSearchExtensions",
            content:"EntityComponentSearchExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/EntityComponentSearchExtensions',
            title:"EntityComponentSearchExtensions",
            description:""
        }
    );
    a(
        {
            id:11,
            title:"Easing",
            content:"Easing",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Mathematics/Easing',
            title:"Easing",
            description:""
        }
    );
    a(
        {
            id:12,
            title:"TransformExtensions",
            content:"TransformExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/TransformExtensions',
            title:"TransformExtensions",
            description:""
        }
    );
    a(
        {
            id:13,
            title:"RaySegment",
            content:"RaySegment",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Mathematics/RaySegment',
            title:"RaySegment",
            description:""
        }
    );
    a(
        {
            id:14,
            title:"RandomListExtensions",
            content:"RandomListExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Collections/RandomListExtensions',
            title:"RandomListExtensions",
            description:""
        }
    );
    a(
        {
            id:15,
            title:"Space",
            content:"Space",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/Space',
            title:"Space",
            description:""
        }
    );
    a(
        {
            id:16,
            title:"DictionaryExtensions",
            content:"DictionaryExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Collections/DictionaryExtensions',
            title:"DictionaryExtensions",
            description:""
        }
    );
    a(
        {
            id:17,
            title:"SimulationExtensions",
            content:"SimulationExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Physics/SimulationExtensions',
            title:"SimulationExtensions",
            description:""
        }
    );
    a(
        {
            id:18,
            title:"ScriptSystemExtensions",
            content:"ScriptSystemExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Engine/ScriptSystemExtensions',
            title:"ScriptSystemExtensions",
            description:""
        }
    );
    a(
        {
            id:19,
            title:"MaterialExtensions",
            content:"MaterialExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Rendering/MaterialExtensions',
            title:"MaterialExtensions",
            description:""
        }
    );
    a(
        {
            id:20,
            title:"EnumerableExtensions",
            content:"EnumerableExtensions",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Collections/EnumerableExtensions',
            title:"EnumerableExtensions",
            description:""
        }
    );
    a(
        {
            id:21,
            title:"EasingFunction",
            content:"EasingFunction",
            description:'',
            tags:''
        },
        {
            url:'/StrideToolkit/api/StrideToolkit.Mathematics/EasingFunction',
            title:"EasingFunction",
            description:""
        }
    );
    var idx = lunr(function() {
        this.field('title');
        this.field('content');
        this.field('description');
        this.field('tags');
        this.ref('id');
        this.use(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
        documents.forEach(function (doc) { this.add(doc) }, this)
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
